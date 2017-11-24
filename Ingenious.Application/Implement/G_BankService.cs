using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Enum;
using Ingenious.Infrastructure.Helper;
using Ingenious.Repositories;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Ingenious.Infrastructure.Extensions;

namespace Ingenious.Application.Implement
{
    public class G_BankService : ApplicationService, IG_BankService 
    {
        private readonly IF_OrderService _IF_OrderService;
        private readonly IG_BankRepository _IG_BankRepository;
        private readonly IG_UserDetailRepository _IG_UserDetailRepository;
        public G_BankService(IRepositoryContext context,
            IG_BankRepository iG_BankRepository, 
            IF_OrderService iF_OrderService,
            IG_UserDetailRepository iG_UserDetailRepository)
            : base(context)
        {
            this._IG_BankRepository = iG_BankRepository;
            this._IF_OrderService = iF_OrderService;
            this._IG_UserDetailRepository = iG_UserDetailRepository;
        } 

        public List<G_BankDTO> GetBanks(string bankCode = "", bool? isAdmin = null,string sort = "order")
        {
            ISpecification<G_Bank> spec = Specification<G_Bank>.Eval(item => true);
            spec = new AndSpecification<G_Bank>(spec,
                Specification<G_Bank>.Eval(item =>
                bankCode == null || bankCode == "" || item.Code.Equals(bankCode)
                ));
            spec = new AndSpecification<G_Bank>(spec,
                Specification<G_Bank>.Eval(item =>
                isAdmin == null || item.IsAdmin.Equals(isAdmin.Value)
                ));

            spec = new AndSpecification<G_Bank>(spec,
                Specification<G_Bank>.Eval(item => item.IsActive));

            var list = new List<G_BankDTO>();
            this._IG_BankRepository.GetAll(spec,sort).ToList().ForEach(item =>
                list.Add(Mapper.Map<G_Bank, G_BankDTO>(item))
                );
            return list;
        }

        /// <summary>
        /// 根据银行编码获取银行用户
        /// </summary>
        /// <param name="bankCode">银行编码</param>
        /// <returns></returns>
        public List<G_UserDetailDTO> GetUserByBank(string bankCode)
        {
            ISpecification<G_UserDetail> spec = Specification<G_UserDetail>.Eval(item => true);
            spec = new AndSpecification<G_UserDetail>(spec,
                Specification<G_UserDetail>.Eval(item =>
                bankCode == null || bankCode == "" || item.BankCode.Equals(bankCode)
                ));
            spec = new AndSpecification<G_UserDetail>(spec,
                Specification<G_UserDetail>.Eval(item =>
                 item.G_User.UserType == G_UserTypeEnum.BC
                 || item.G_User.UserType== G_UserTypeEnum.BM
                ));

            var list = new List<G_UserDetailDTO>();
            this._IG_UserDetailRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<G_UserDetail, G_UserDetailDTO>(item))
                );
            return list;
        }

        public G_BankDTO GetBank(string bankCode)
        {
            return this.GetBanks(bankCode).FirstOrDefault();
        }


        public G_BankDTO GetBankByUserId(Guid id)
        {
            return Mapper.Map<G_Bank, G_BankDTO>(this._IG_BankRepository.GetBankByUserId(id));
        }


        public string AssignOrderToClerk(string bankCode)
        {
            var list = this._IG_BankRepository.AssignOrderToClerk(bankCode).ToList();

            if (list.Count() == 0)
            {
                return string.Empty;
            }

            var count = list.Min<KeyValue<string, int>>(i => i.Value);

            var obj = list.Where<KeyValue<string, int>>(item => item.Value == count).FirstOrDefault();

            if(obj==null)
            {
                return string.Empty;
            }
            return obj.Key;
        }
        
        public DTO.G_BankDTO GetByKey(System.Guid id)
        {
            var user = this._IG_BankRepository.GetByKey(id);

            var userDTO = Mapper.Map<G_Bank, G_BankDTO>(user);

            return userDTO;
        }

        public G_BankDTO Create(G_BankDTO dto)
        {
            int maxOrder = this._IG_BankRepository.GetMaxCode();

            dto.Order = maxOrder + 1;
            var user= base.F_Create<G_BankDTO, G_Bank>(dto
                , _IG_BankRepository
                , dtoAction => { });
            return user;
        }

        public List<G_BankDTO> Update(System.Collections.Generic.List<G_BankDTO> dtoList)
        {
            var list = new List<G_BankDTO>();
           
                base.F_Update<G_BankDTO, List<G_BankDTO>, G_Bank>(dtoList
                 , _IG_BankRepository
                 , dto => dto.Id
                 , (dto, entity) =>
                 {
                     entity.Order = dto.Order;
                     entity.Logo = dto.Logo;
                     entity.Code = dto.Code;
                     entity.IsAdmin = dto.IsAdmin;
                     entity.IsAssignAuto = dto.IsAssignAuto;
                     entity.Name = dto.Name;
                     entity.ParentId = dto.ParentId;
                     entity.ModifiedBy = dto.ModifiedBy;
                 });
           
            return list;
        }


        public void Delete(System.Collections.Generic.List<G_BankDTO> dtoList)
        {
            base.F_Update<G_BankDTO, List<G_BankDTO>, G_Bank>(dtoList
                , _IG_BankRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

    }
}
