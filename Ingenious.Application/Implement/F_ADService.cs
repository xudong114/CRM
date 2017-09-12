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
using System.Data.Entity.SqlServer;
using System.Linq;

namespace Ingenious.Application.Implement
{
    public class F_ADService : ApplicationService, IF_ADService 
    {
        private readonly IF_ADRepository _IF_ADRepository;
        public F_ADService(IRepositoryContext context,
            IF_ADRepository iF_ADRepository)
            : base(context)
        {
            this._IF_ADRepository = iF_ADRepository;
        }

        public List<F_ADDTO> GetAll(string code)
        {
            ISpecification<F_AD> spec = Specification<F_AD>.Eval(item => true);
            spec = new AndSpecification<F_AD>(spec,
                Specification<F_AD>.Eval(item =>
                  code == "" || item.Code.Equals(code)));
            spec = new AndSpecification<F_AD>(spec,
                Specification<F_AD>.Eval(item => item.IsActive));

            spec = new AndSpecification<F_AD>(spec,
               Specification<F_AD>.Eval(item =>
               !item.BeginDate.HasValue ||
               item.BeginDate.HasValue && item.BeginDate.Value <= SqlFunctions.GetDate().Value
               ));

            spec = new AndSpecification<F_AD>(spec,
               Specification<F_AD>.Eval(item =>
               !item.EndDate.HasValue ||
               item.EndDate.HasValue && item.EndDate.Value >= SqlFunctions.GetDate().Value
               ));


            var list = new List<F_ADDTO>();
            this._IF_ADRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<F_AD, F_ADDTO>(item))
                );
            return list;
        }

        public List<F_ADDTO> GetAll(string []codes)
        {
            ISpecification<F_AD> spec = Specification<F_AD>.Eval(item => true);
            spec = new AndSpecification<F_AD>(spec,
                Specification<F_AD>.Eval(item => codes.Contains(item.Code)));
            spec = new AndSpecification<F_AD>(spec,
                Specification<F_AD>.Eval(item => item.IsActive));

            spec = new AndSpecification<F_AD>(spec,
               Specification<F_AD>.Eval(item =>
               !item.BeginDate.HasValue ||
               item.BeginDate.HasValue && item.BeginDate.Value <= SqlFunctions.GetDate().Value
               ));

            spec = new AndSpecification<F_AD>(spec,
               Specification<F_AD>.Eval(item =>
               !item.EndDate.HasValue ||
               item.EndDate.HasValue && item.EndDate.Value >= SqlFunctions.GetDate().Value
               ));


            var list = new List<F_ADDTO>();
            this._IF_ADRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<F_AD, F_ADDTO>(item))
                );
            return list;
        }

        public DTO.F_ADDTO GetByKey(System.Guid id)
        {
            var ad = this._IF_ADRepository.GetByKey(id);

            var adDTO = Mapper.Map<F_AD, F_ADDTO>(ad);

            return adDTO;
        }

        public F_ADDTO Create(F_ADDTO dto)
        {
            var account = base.F_Create<F_ADDTO, F_AD>(dto
                , _IF_ADRepository
                , dtoAction => { });
            return account;
        }

        public List<F_ADDTO> Update(System.Collections.Generic.List<F_ADDTO> dtoList)
        {
            return base.F_Update<F_ADDTO, List<F_ADDTO>, F_AD>(dtoList
                , _IF_ADRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.Name = dto.Name;
                    entity.Remark = dto.Remark;
                    entity.Url = dto.Url;
                    entity.Image = dto.Image;
                    entity.BeginDate = dto.BeginDate;
                    entity.EndDate = dto.EndDate;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public void Delete(System.Collections.Generic.List<F_ADDTO> dtoList)
        {
            base.F_Update<F_ADDTO, List<F_ADDTO>, F_AD>(dtoList
                , _IF_ADRepository
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
