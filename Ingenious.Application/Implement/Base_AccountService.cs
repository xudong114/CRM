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
    public class Base_AccountService : ApplicationService, IBase_AccountService
    {
        private readonly IBase_AccountRepository _IBase_AccountRepository;
        public Base_AccountService(IRepositoryContext context,
            IBase_AccountRepository iBase_AccountRepository)
            : base(context)
        {
            this._IBase_AccountRepository = iBase_AccountRepository;
        }

        /// <summary>
        /// 根据身份证号码获取账户信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        public Base_AccountDTOList GetAccountByIDNo(string idNo)
        {
            var list = new Base_AccountDTOList();
            this._IBase_AccountRepository.GetAccountByIDNo(idNo).ToList().ForEach(item =>
                list.Add(Mapper.Map<Base_Account, Base_AccountDTO>(item))
            );
            return list;
        }

        public DTO.Base_AccountDTO GetByKey(System.Guid id)
        {
            var model = this._IBase_AccountRepository.GetByKey(id);

            var modelDTO = Mapper.Map<Base_Account, Base_AccountDTO>(model);

            return modelDTO;
        }

        public Base_AccountDTO Create(Base_AccountDTO dto)
        {
            var user = base.F_Create<Base_AccountDTO, Base_Account>(dto
                , _IBase_AccountRepository
                , dtoAction => { });

            return user;
        }

        public List<Base_AccountDTO> Update(System.Collections.Generic.List<Base_AccountDTO> dtoList)
        {
            var list = new List<Base_AccountDTO>();

            base.F_Update<Base_AccountDTO, List<Base_AccountDTO>, Base_Account>(dtoList
             , _IBase_AccountRepository
             , dto => dto.Id
             , (dto, entity) =>
             {
                 entity.VirtualNo = dto.VirtualNo;
                 entity.BankCode = dto.BankCode;
                 entity.IDNo = dto.IDNo;
                 entity.InlineNo = dto.InlineNo;
                 entity.IsDefault = dto.IsDefault;
                 entity.Remark = dto.Remark;
                 entity.IsActive = dto.IsActive;
                 entity.ModifiedBy = dto.ModifiedBy;
             });

            return list;
        }


        public void Delete(System.Collections.Generic.List<Base_AccountDTO> dtoList)
        {
            base.F_Update<Base_AccountDTO, List<Base_AccountDTO>, Base_Account>(dtoList
                , _IBase_AccountRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


    }
}
