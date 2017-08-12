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
    public class F_ActivityService : ApplicationService, IF_ActivityService
    {
        private readonly IF_ActivityRepository _IF_ActivityRepository;
        public F_ActivityService(IRepositoryContext context,
            IF_ActivityRepository IF_ActivityRepository)
            : base(context)
        {
            this._IF_ActivityRepository = IF_ActivityRepository;
        }


        public DTO.F_ActivityDTO GetByKey(System.Guid id)
        {
            var account = this._IF_ActivityRepository.GetByKey(id);

            var accountDTO = Mapper.Map<F_Activity, F_ActivityDTO>(account);

            return accountDTO;
        }

        public int GetMessageCount(Guid referenceId)
        {
            ISpecification<F_Activity> spec = Specification<F_Activity>.Eval(item => true);
            spec = new AndSpecification<F_Activity>(spec,
                Specification<F_Activity>.Eval(item =>
                  referenceId == null || item.ReferenceId.Equals(referenceId)));
            spec = new AndSpecification<F_Activity>(spec,
                Specification<F_Activity>.Eval(item => !item.IsRead));
            spec = new AndSpecification<F_Activity>(spec,
                Specification<F_Activity>.Eval(item => SqlFunctions.DateDiff("day", item.CreatedDate, SqlFunctions.GetDate()) <= 7));

            return this._IF_ActivityRepository.GetAll(spec).Count();
        }

        public List<F_ActivityDTO> GetAll(Guid? referenceId, bool? isRead, bool? isGlobal)
        {
            ISpecification<F_Activity> spec = Specification<F_Activity>.Eval(item => true);
            spec = new AndSpecification<F_Activity>(spec,
                Specification<F_Activity>.Eval(item =>
                  referenceId == null || item.ReferenceId.Equals(referenceId.Value)));
            spec = new AndSpecification<F_Activity>(spec,
                Specification<F_Activity>.Eval(item =>
                  isRead == null || item.IsRead.Equals(isRead.Value)));
            spec = new AndSpecification<F_Activity>(spec,
                Specification<F_Activity>.Eval(item =>
                  isGlobal == null || item.IsGlobal.Equals(isGlobal.Value)));

            var list = new List<F_ActivityDTO>();
            this._IF_ActivityRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<F_Activity, F_ActivityDTO>(item))
                );
            return list;
        }

        public F_ActivityDTO Create(F_ActivityDTO dto)
        {
            var account = base.F_Create<F_ActivityDTO, F_Activity>(dto
                , _IF_ActivityRepository
                , dtoAction => { });
            return account;
        }

        public List<F_ActivityDTO> UpdateAlipay(System.Collections.Generic.List<F_ActivityDTO> dtoList)
        {
            return base.F_Update<F_ActivityDTO, List<F_ActivityDTO>, F_Activity>(dtoList
                , _IF_ActivityRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public List<F_ActivityDTO> Update(System.Collections.Generic.List<F_ActivityDTO> dtoList)
        {
            return base.F_Update<F_ActivityDTO, List<F_ActivityDTO>, F_Activity>(dtoList
                , _IF_ActivityRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsRead = dto.IsRead;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public void Delete(System.Collections.Generic.List<F_ActivityDTO> dtoList)
        {
            base.F_Update<F_ActivityDTO, List<F_ActivityDTO>, F_Activity>(dtoList
                , _IF_ActivityRepository
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
