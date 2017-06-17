using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Repositories;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Implement
{
    public class ActivityService : ApplicationService, IActivityService
    {
        private readonly IActivityRepository _IActivityRepository;
        private readonly IUserRepository _IUserRepository;
        public ActivityService(IRepositoryContext context,
            IActivityRepository iActivityRepository,
            IUserRepository iUserRepository)
            : base(context)
        {
            this._IActivityRepository = iActivityRepository;
            this._IUserRepository = iUserRepository;
        }

        public ActivityDTOList GetAll(Guid? referenceId, Guid? userId, string sort = "created_desc")
        {
            var list = new ActivityDTOList();

            ISpecification<Activity> spec = Specification<Activity>.Eval(item => true);
            spec = new AndSpecification<Activity>(spec,
                Specification<Activity>.Eval(item=> !referenceId.HasValue || item.ReferenceId==referenceId.Value));
            spec = new AndSpecification<Activity>(spec,
                Specification<Activity>.Eval(item => !userId.HasValue || item.UserId == userId.Value));
            this._IActivityRepository.GetAll(spec).OrderByDescending(model=>model.CreatedDate).ToList().ForEach(item=>
                    list.Add(Mapper.Map<Activity, ActivityDTO>(item))
                );
            this.AppendUserInfo(list, this._IUserRepository.Data);
            return list;
        }

        public ActivityDTO GetByKey(Guid id)
        {
            var model = this._IActivityRepository.GetByKey(id);
            var dto = Mapper.Map<Activity, ActivityDTO>(model);
            this.AppendUserInfo(dto, this._IUserRepository.Data);
            return dto;
        }

        public ActivityDTO Create(ActivityDTO dto)
        {
            return base.Create<ActivityDTO, Activity>(dto
                    , _IActivityRepository
                    , dtoAction => { });
        }

        public List<ActivityDTO> Update(List<ActivityDTO> dtoList)
        {
            return base.Update<ActivityDTO, List<ActivityDTO>, Activity>(dtoList
                    , _IActivityRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        if(!string.IsNullOrWhiteSpace(dto.Content))
                        {
                            entity.Content = dto.Content;
                        }
                        entity.ModifiedBy = dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                        entity.Version = dto.Version;
                    });
        }

        public void Delete(List<ActivityDTO> dtoList)
        {
            base.Update<ActivityDTO, List<ActivityDTO>, Activity>(dtoList
                , _IActivityRepository
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
