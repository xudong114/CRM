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

namespace Ingenious.Application.Implement
{
    public class F_BankService : ApplicationService, IF_BankService 
    {
        private readonly IF_OrderService _IF_OrderService;
        private readonly IF_BankRepository _IF_BankRepository;
        public F_BankService(IRepositoryContext context,
            IF_BankRepository iF_BankRepository, 
            IF_OrderService iF_OrderService)
            : base(context)
        {
            this._IF_BankRepository = iF_BankRepository;
            this._IF_OrderService = iF_OrderService;
        }

        public List<F_BankDTO> GetBanks(string bankCode = "", bool? isAdmin = null,string sort = "order")
        {
            ISpecification<F_Bank> spec = Specification<F_Bank>.Eval(item => true);
            spec = new AndSpecification<F_Bank>(spec,
                Specification<F_Bank>.Eval(item =>
                bankCode == null || bankCode == "" || item.Code.Equals(bankCode)
                ));
            spec = new AndSpecification<F_Bank>(spec,
                Specification<F_Bank>.Eval(item =>
                isAdmin == null || item.IsAdmin.Equals(isAdmin.Value)
                ));

            spec = new AndSpecification<F_Bank>(spec,
                Specification<F_Bank>.Eval(item => item.IsActive));

            var list = new List<F_BankDTO>();
            this._IF_BankRepository.GetAll(spec,sort).ToList().ForEach(item =>
                list.Add(Mapper.Map<F_Bank, F_BankDTO>(item))
                );
            return list;
        }


        public F_BankDTO GetBank(string bankCode)
        {
            return this.GetBanks(bankCode).FirstOrDefault();
        }


        public F_BankDTO GetBankByUserId(Guid id)
        {
            return Mapper.Map<F_Bank, F_BankDTO>(this._IF_BankRepository.GetBankByUserId(id));
        }


        public string AssignOrderToClerk(string bankCode)
        {
            var list = this._IF_BankRepository.AssignOrderToClerk(bankCode).ToList();

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

        public DTO.F_BankDTO GetByKey(System.Guid id)
        {
            var user = this._IF_BankRepository.GetByKey(id);

            var userDTO = Mapper.Map<F_Bank, F_BankDTO>(user);

            return userDTO;
        }

        public F_BankDTO Create(F_BankDTO dto)
        {
            int maxOrder = this._IF_BankRepository.Data.Max(item => item.Order);
            dto.Order = maxOrder + 1;
            var user= base.F_Create<F_BankDTO, F_Bank>(dto
                , _IF_BankRepository
                , dtoAction => { });
            return user;
        }

        public List<F_BankDTO> Update(System.Collections.Generic.List<F_BankDTO> dtoList)
        {
            var list = new List<F_BankDTO>();
           
                base.F_Update<F_BankDTO, List<F_BankDTO>, F_Bank>(dtoList
                 , _IF_BankRepository
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


        public void Delete(System.Collections.Generic.List<F_BankDTO> dtoList)
        {
            base.F_Update<F_BankDTO, List<F_BankDTO>, F_Bank>(dtoList
                , _IF_BankRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

    }
}
