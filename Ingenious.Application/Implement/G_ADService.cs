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
    public class G_ADService : ApplicationService, IG_ADService 
    {
        private readonly IG_ADRepository _IG_ADRepository;
        public G_ADService(IRepositoryContext context,
            IG_ADRepository iG_ADRepository)
            : base(context)
        {
            this._IG_ADRepository = iG_ADRepository;
        }

        public List<G_ADDTO> GetAll(string code)
        {
            ISpecification<G_AD> spec = Specification<G_AD>.Eval(item => true);
            spec = new AndSpecification<G_AD>(spec,
                Specification<G_AD>.Eval(item =>
                  code == "" || item.Code.Equals(code)));
            spec = new AndSpecification<G_AD>(spec,
                Specification<G_AD>.Eval(item => item.IsActive));

            spec = new AndSpecification<G_AD>(spec,
               Specification<G_AD>.Eval(item =>
               !item.BeginDate.HasValue ||
               item.BeginDate.HasValue && item.BeginDate.Value <= SqlFunctions.GetDate().Value
               ));

            spec = new AndSpecification<G_AD>(spec,
               Specification<G_AD>.Eval(item =>
               !item.EndDate.HasValue ||
               item.EndDate.HasValue && item.EndDate.Value >= SqlFunctions.GetDate().Value
               ));


            var list = new List<G_ADDTO>();
            this._IG_ADRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<G_AD, G_ADDTO>(item))
                );
            return list;
        }

        public List<G_ADDTO> GetAll(string []codes)
        {
            ISpecification<G_AD> spec = Specification<G_AD>.Eval(item => true);
            spec = new AndSpecification<G_AD>(spec,
                Specification<G_AD>.Eval(item => codes.Contains(item.Code)));
            spec = new AndSpecification<G_AD>(spec,
                Specification<G_AD>.Eval(item => item.IsActive));

            spec = new AndSpecification<G_AD>(spec,
               Specification<G_AD>.Eval(item =>
               !item.BeginDate.HasValue ||
               item.BeginDate.HasValue && item.BeginDate.Value <= SqlFunctions.GetDate().Value
               ));

            spec = new AndSpecification<G_AD>(spec,
               Specification<G_AD>.Eval(item =>
               !item.EndDate.HasValue ||
               item.EndDate.HasValue && item.EndDate.Value >= SqlFunctions.GetDate().Value
               ));


            var list = new List<G_ADDTO>();
            this._IG_ADRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<G_AD, G_ADDTO>(item))
                );
            return list;
        }

        public DTO.G_ADDTO GetByKey(System.Guid id)
        {
            var ad = this._IG_ADRepository.GetByKey(id);

            var adDTO = Mapper.Map<G_AD, G_ADDTO>(ad);

            return adDTO;
        }

        public G_ADDTO Create(G_ADDTO dto)
        {
            var account = base.F_Create<G_ADDTO, G_AD>(dto
                , _IG_ADRepository
                , dtoAction => { });
            return account;
        }

        public List<G_ADDTO> Update(System.Collections.Generic.List<G_ADDTO> dtoList)
        {
            return base.F_Update<G_ADDTO, List<G_ADDTO>, G_AD>(dtoList
                , _IG_ADRepository
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

        public void Delete(System.Collections.Generic.List<G_ADDTO> dtoList)
        {
            base.F_Update<G_ADDTO, List<G_ADDTO>, G_AD>(dtoList
                , _IG_ADRepository
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
