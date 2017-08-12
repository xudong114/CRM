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
    public class F_AccountService : ApplicationService, IF_AccountService 
    {
        private readonly IF_AccountRepository _IF_AccountRepository;
        public F_AccountService(IRepositoryContext context,
            IF_AccountRepository iF_AccountRepository)
            : base(context)
        {
            this._IF_AccountRepository = iF_AccountRepository;
        }

        public DTO.F_AccountDTO GetByKey(System.Guid id)
        {
            var account = this._IF_AccountRepository.GetByKey(id);

            var accountDTO = Mapper.Map<F_Account, F_AccountDTO>(account);

            return accountDTO;
        }
        /// <summary>
        /// 获取导购员账户
        /// </summary>
        /// <param name="userId">导购员账户Id(F_Account.Id)</param>
        /// <returns></returns>
        public F_AccountDTO GetAccount(Guid userId)
        {
            var account = this._IF_AccountRepository.GetAccount(userId);
            return Mapper.Map<F_Account, F_AccountDTO>(account);
        }

        /// <summary>
        /// 设置支付宝账号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public F_AccountDTO SetAlipay(F_AccountDTO account)
        {
            var model = this._IF_AccountRepository.GetAccount(account.UserId);
            if (model == null)
            {
                return this.Create(account);
            }
            else
            {
                return this.UpdateAlipay(new List<F_AccountDTO> { account }).FirstOrDefault();
            }
        }

        /// <summary>
        /// 更新账户余额和总收益
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public F_AccountDTO ResetBalance(F_AccountDTO account)
        {
            return this.Update(new List<F_AccountDTO> { account }).FirstOrDefault();
        }

        public F_AccountDTO Create(F_AccountDTO dto)
        {
            var account = base.F_Create<F_AccountDTO, F_Account>(dto
                , _IF_AccountRepository
                , dtoAction => { });
            return account;
        }

        public List<F_AccountDTO> UpdateAlipay(System.Collections.Generic.List<F_AccountDTO> dtoList)
        {
            return base.F_Update<F_AccountDTO, List<F_AccountDTO>, F_Account>(dtoList
                , _IF_AccountRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.Alipay = dto.Alipay;
                    entity.Name = dto.Name;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public List<F_AccountDTO> Update(System.Collections.Generic.List<F_AccountDTO> dtoList)
        {
            return base.F_Update<F_AccountDTO, List<F_AccountDTO>, F_Account>(dtoList
                , _IF_AccountRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.TotalAmount = dto.TotalAmount;
                    entity.Balance = dto.Balance;

                    entity.UserId = dto.UserId;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public void Delete(System.Collections.Generic.List<F_AccountDTO> dtoList)
        {
            base.F_Update<F_AccountDTO, List<F_AccountDTO>, F_Account>(dtoList
                , _IF_AccountRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

    }
}
