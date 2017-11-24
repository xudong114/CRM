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
    public class G_ActivityService : ApplicationService, IG_ActivityService
    {
        private readonly IG_ActivityRepository _IG_ActivityRepository;
        public G_ActivityService(IRepositoryContext context,
            IG_ActivityRepository IG_ActivityRepository)
            : base(context)
        {
            this._IG_ActivityRepository = IG_ActivityRepository;
        }

        public int GetMessageCount(Guid referenceId)
        {
            ISpecification<G_Activity> spec = Specification<G_Activity>.Eval(item => true);
            spec = new AndSpecification<G_Activity>(spec,
                Specification<G_Activity>.Eval(item =>
                  referenceId == null || item.ReferenceId.Equals(referenceId)));
            spec = new AndSpecification<G_Activity>(spec,
                Specification<G_Activity>.Eval(item => !item.IsRead));
            spec = new AndSpecification<G_Activity>(spec,
                Specification<G_Activity>.Eval(item => SqlFunctions.DateDiff("day", item.CreatedDate, SqlFunctions.GetDate()) <= 7));

            return this._IG_ActivityRepository.GetAll(1,1,spec).Data.Count();
        }

        public G_ActivityDTOListWithPagination GetAll(int pageIndex, int pageSize, Guid? referenceId, bool? isRead, bool? isGlobal,string sort="createddate_desc")
        {
            ISpecification<G_Activity> spec = Specification<G_Activity>.Eval(item => true);
            spec = new AndSpecification<G_Activity>(spec,
                Specification<G_Activity>.Eval(item =>
                  referenceId == null || item.ReferenceId.Equals(referenceId.Value)));
            spec = new AndSpecification<G_Activity>(spec,
                Specification<G_Activity>.Eval(item =>
                  isRead == null || item.IsRead.Equals(isRead.Value)));
            spec = new AndSpecification<G_Activity>(spec,
                Specification<G_Activity>.Eval(item =>
                  isGlobal == null || item.IsGlobal.Equals(isGlobal.Value)));

            var list = new List<G_ActivityDTO>();
            var result = this._IG_ActivityRepository.GetAll(pageIndex,pageSize,spec,sort);
            
            int totalPages = 0;
            int totalRecords = 0;
            if (result != null)
            {
                result.Data.ForEach(item =>
                    list.Add(Mapper.Map<G_Activity, G_ActivityDTO>(item))
                );
                totalPages = result.TotalPages;
                totalRecords = result.TotalRecords;
            }

            return new G_ActivityDTOListWithPagination
            {
                TotalRecords = totalRecords,
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Data = list
            };
        }

        public DTO.G_ActivityDTO GetByKey(System.Guid id)
        {
            var account = this._IG_ActivityRepository.GetByKey(id);

            var accountDTO = Mapper.Map<G_Activity, G_ActivityDTO>(account);

            return accountDTO;
        }


        public G_ActivityDTO Create(G_ActivityDTO dto)
        {
            var account = base.F_Create<G_ActivityDTO, G_Activity>(dto
                , _IG_ActivityRepository
                , dtoAction => { });
            return account;
        }

        public List<G_ActivityDTO> UpdateAlipay(System.Collections.Generic.List<G_ActivityDTO> dtoList)
        {
            return base.F_Update<G_ActivityDTO, List<G_ActivityDTO>, G_Activity>(dtoList
                , _IG_ActivityRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public List<G_ActivityDTO> Update(System.Collections.Generic.List<G_ActivityDTO> dtoList)
        {
            return base.F_Update<G_ActivityDTO, List<G_ActivityDTO>, G_Activity>(dtoList
                , _IG_ActivityRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsRead = dto.IsRead;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public void Delete(System.Collections.Generic.List<G_ActivityDTO> dtoList)
        {
            base.F_Update<G_ActivityDTO, List<G_ActivityDTO>, G_Activity>(dtoList
                , _IG_ActivityRepository
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
