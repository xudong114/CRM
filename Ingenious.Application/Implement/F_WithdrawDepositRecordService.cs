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
    public class F_WithdrawDepositRecordService : ApplicationService, IF_WithdrawDepositRecordService
    {
        private readonly IF_WithdrawDepositRecordRepository _IF_WithdrawDepositRecordRepository;
        public F_WithdrawDepositRecordService(IRepositoryContext context,
            IF_WithdrawDepositRecordRepository iF_WithdrawDepositRecordRepository)
            : base(context)
        {
            this._IF_WithdrawDepositRecordRepository = iF_WithdrawDepositRecordRepository;
        }
        /// <summary>
        /// 查询提现记录
        /// </summary>
        /// <param name="clerkCode"></param>
        /// <returns></returns>
        public F_WithdrawDepositRecordDTOList GetAll(string clerkCode = "")
        {
            ISpecification<F_WithdrawDepositRecord> spec = Specification<F_WithdrawDepositRecord>.Eval(item => true);
            spec = new AndSpecification<F_WithdrawDepositRecord>(spec,
                Specification<F_WithdrawDepositRecord>.Eval(item =>
                  clerkCode == null || clerkCode == "" || item.ClerkCode.Equals(clerkCode)));
            var list = new F_WithdrawDepositRecordDTOList();
            this._IF_WithdrawDepositRecordRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<F_WithdrawDepositRecord, F_WithdrawDepositRecordDTO>(item))
                );
            return list;
        }

        public DTO.F_WithdrawDepositRecordDTO GetByKey(System.Guid id)
        {
            var user = this._IF_WithdrawDepositRecordRepository.GetByKey(id);

            var userDTO = Mapper.Map<F_WithdrawDepositRecord, F_WithdrawDepositRecordDTO>(user);

            return userDTO;
        }

        public F_WithdrawDepositRecordDTO Create(F_WithdrawDepositRecordDTO dto)
        {
            var user = base.F_Create<F_WithdrawDepositRecordDTO, F_WithdrawDepositRecord>(dto
                , _IF_WithdrawDepositRecordRepository
                , dtoAction => { });

            return user;
        }

        public List<F_WithdrawDepositRecordDTO> Update(System.Collections.Generic.List<F_WithdrawDepositRecordDTO> dtoList)
        {
            return base.F_Update<F_WithdrawDepositRecordDTO, List<F_WithdrawDepositRecordDTO>, F_WithdrawDepositRecord>(dtoList
                , _IF_WithdrawDepositRecordRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<F_WithdrawDepositRecordDTO> dtoList)
        {
            base.F_Update<F_WithdrawDepositRecordDTO, List<F_WithdrawDepositRecordDTO>, F_WithdrawDepositRecord>(dtoList
                , _IF_WithdrawDepositRecordRepository
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
