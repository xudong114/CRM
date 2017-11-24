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
    public class G_LoanProductService : ApplicationService, IG_LoanProductService 
    {
        private readonly IG_LoanProductRepository _IG_LoanProductRepository;
        public G_LoanProductService(IRepositoryContext context,
            IG_LoanProductRepository iG_LoanProductRepository)
            : base(context)
        {
            this._IG_LoanProductRepository = iG_LoanProductRepository;
        }

        public List<G_LoanProductDTO> GetAll(string code = "", bool? isActive = true)
        {
            ISpecification<G_LoanProduct> spec = Specification<G_LoanProduct>.Eval(item => true);
            spec = new AndSpecification<G_LoanProduct>(spec,
                Specification<G_LoanProduct>.Eval(item =>
                  code == "" || item.Code.Equals(code)));
            spec = new AndSpecification<G_LoanProduct>(spec,
                Specification<G_LoanProduct>.Eval(item =>
                    isActive == null || item.IsActive.Equals(isActive.Value)));

            var list = new List<G_LoanProductDTO>();
            this._IG_LoanProductRepository.GetAll(spec).ToList().ForEach(item =>
               list.Add(Mapper.Map<G_LoanProduct, G_LoanProductDTO>(item))
               );
            return list;
        }

        public DTO.G_LoanProductDTO GetByKey(System.Guid id)
        {
            var user = this._IG_LoanProductRepository.GetByKey(id);

            var userDTO = Mapper.Map<G_LoanProduct, G_LoanProductDTO>(user);

            return userDTO;
        }

        public G_LoanProductDTO Create(G_LoanProductDTO dto)
        {
            return base.F_Create<G_LoanProductDTO, G_LoanProduct>(dto
                , _IG_LoanProductRepository
                , dtoAction => { }); 
        }

        public List<G_LoanProductDTO> Update(System.Collections.Generic.List<G_LoanProductDTO> dtoList)
        {
            return base.F_Update<G_LoanProductDTO, List<G_LoanProductDTO>, G_LoanProduct>(dtoList
                , _IG_LoanProductRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.Code = dto.Code;
                    entity.Loan = dto.Loan;
                    entity.Logo = dto.Logo;
                    entity.Name = dto.Name;
                    entity.Policy = dto.Policy;
                    entity.Rate = dto.Rate;
                    entity.Terms = dto.Terms;
                    entity.Tip = dto.Tip;
                    
                    //entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<G_LoanProductDTO> dtoList)
        {
            base.F_Update<G_LoanProductDTO, List<G_LoanProductDTO>, G_LoanProduct>(dtoList
                , _IG_LoanProductRepository
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
