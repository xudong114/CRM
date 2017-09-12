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
    public class F_CreditCardApplicationService : ApplicationService, IF_CreditCardApplicationService
    {
        private readonly IF_CreditCardApplicationRepository _IF_CreditCardApplicationRepository;
        public F_CreditCardApplicationService(IRepositoryContext context,
            IF_CreditCardApplicationRepository iF_CreditCardApplicationRepository)
            : base(context)
        {
            this._IF_CreditCardApplicationRepository = iF_CreditCardApplicationRepository;
        }

        public List<F_CreditCardApplicationDTO> GetAll()
        {
            ISpecification<F_CreditCardApplication> spec = Specification<F_CreditCardApplication>.Eval(item => true);

            spec = new AndSpecification<F_CreditCardApplication>(spec,
               Specification<F_CreditCardApplication>.Eval(item => item.IsActive));

            var list = new List<F_CreditCardApplicationDTO>();
            this._IF_CreditCardApplicationRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<F_CreditCardApplication, F_CreditCardApplicationDTO>(item))
                );
            return list;
        }

        public DTO.F_CreditCardApplicationDTO GetByKey(System.Guid id)
        {
            var user = this._IF_CreditCardApplicationRepository.GetByKey(id);

            var userDTO = Mapper.Map<F_CreditCardApplication, F_CreditCardApplicationDTO>(user);

            return userDTO;
        }

        public F_CreditCardApplicationDTO Create(F_CreditCardApplicationDTO dto)
        {
            var user= base.F_Create<F_CreditCardApplicationDTO, F_CreditCardApplication>(dto
                , _IF_CreditCardApplicationRepository
                , dtoAction => { });
            return user;
        }

        public List<F_CreditCardApplicationDTO> Update(System.Collections.Generic.List<F_CreditCardApplicationDTO> dtoList)
        {
            var list = new List<F_CreditCardApplicationDTO>();
           
                base.F_Update<F_CreditCardApplicationDTO, List<F_CreditCardApplicationDTO>, F_CreditCardApplication>(dtoList
                 , _IF_CreditCardApplicationRepository
                 , dto => dto.Id
                 , (dto, entity) =>
                 {
                     entity.IsActive = dto.IsActive;
                     entity.ModifiedBy = dto.ModifiedBy;
                 });
           
            return list;
        }


        public void Delete(System.Collections.Generic.List<F_CreditCardApplicationDTO> dtoList)
        {
            base.F_Update<F_CreditCardApplicationDTO, List<F_CreditCardApplicationDTO>, F_CreditCardApplication>(dtoList
                , _IF_CreditCardApplicationRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

    }
}
