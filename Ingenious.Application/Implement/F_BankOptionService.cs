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
    public class F_BankOptionService : ApplicationService, IF_BankOptionService
    {
        private readonly IF_BankOptionRepository _IF_BankOptionRepository;
        public F_BankOptionService(IRepositoryContext context,
            IF_BankOptionRepository iF_BankOptionRepository)
            : base(context)
        {
            this._IF_BankOptionRepository = iF_BankOptionRepository;
        }

        public List<F_BankOptionDTO> GetAll()
        {
            ISpecification<F_BankOption> spec = Specification<F_BankOption>.Eval(item => true);
            //spec = new AndSpecification<F_BankOption>(spec,
            //    Specification<F_BankOption>.Eval(item =>
            //    bankCode == null || bankCode == "" || item.Code.Equals(bankCode)
            //    ));
            //spec = new AndSpecification<F_BankOption>(spec,
            //    Specification<F_BankOption>.Eval(item =>
            //    isAdmin == null || isAdmin.HasValue || item.IsAdmin.Equals(isAdmin.Value)
            //    ));
            var list = new List<F_BankOptionDTO>();
            this._IF_BankOptionRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<F_BankOption, F_BankOptionDTO>(item))
                );
            return list;
        }


        public F_BankOptionDTO GetBankOptionByBankId(Guid bankId)
        {
            return Mapper.Map<F_BankOption, F_BankOptionDTO>(this._IF_BankOptionRepository.GetBankOptionByBankId(bankId));
        }

        public DTO.F_BankOptionDTO GetByKey(System.Guid id)
        {
            var user = this._IF_BankOptionRepository.GetByKey(id);

            var userDTO = Mapper.Map<F_BankOption, F_BankOptionDTO>(user);

            return userDTO;
        }

        public F_BankOptionDTO Create(F_BankOptionDTO dto)
        {
            var user= base.F_Create<F_BankOptionDTO, F_BankOption>(dto
                , _IF_BankOptionRepository
                , dtoAction => { });
            return user;
        }

        public List<F_BankOptionDTO> Update(System.Collections.Generic.List<F_BankOptionDTO> dtoList)
        {
            var list = new List<F_BankOptionDTO>();
           
                base.F_Update<F_BankOptionDTO, List<F_BankOptionDTO>, F_BankOption>(dtoList
                 , _IF_BankOptionRepository
                 , dto => dto.Id
                 , (dto, entity) =>
                 {
                     entity.Loan = dto.Loan;
                     entity.Policy = dto.Policy;
                     entity.Rate = dto.Rate;
                     entity.Terms = dto.Terms;
                     entity.Workflow = dto.Workflow;
                     entity.IsActive = dto.IsActive;
                     entity.ModifiedBy = dto.ModifiedBy;
                 });
           
            return list;
        }


        public void Delete(System.Collections.Generic.List<F_BankOptionDTO> dtoList)
        {
            base.F_Update<F_BankOptionDTO, List<F_BankOptionDTO>, F_BankOption>(dtoList
                , _IF_BankOptionRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

    }
}
