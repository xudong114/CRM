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
    public class ActivityCategoryService : ApplicationService, IActivityCategoryService
    {
        private readonly IActivityCategoryRepository _IActivityCategoryRepository;
        public ActivityCategoryService(IRepositoryContext context,
            IActivityCategoryRepository iActivityCategoryRepository)
            : base(context)
        {
            this._IActivityCategoryRepository = iActivityCategoryRepository;
        }

        public DTO.ActivityCategoryDTOList GetAll(string sort = "created_desc")
        {
            var list = new ActivityCategoryDTOList();

            ISpecification<ActivityCategoryDTO> spec = Specification<ActivityCategoryDTO>.Eval(item => true);

            this._IActivityCategoryRepository.GetAll().OrderByDescending(model=>model.CreatedDate).ToList().ForEach(item =>
                    list.Add(Mapper.Map<ActivityCategory, ActivityCategoryDTO>(item))
                );
            return list;
        }

        public ActivityCategoryDTO GetByKey(Guid id)
        {
            var model = this._IActivityCategoryRepository.GetByKey(id);

            return Mapper.Map<ActivityCategory, ActivityCategoryDTO>(model);
        }

        public DTO.ActivityCategoryDTO Create(DTO.ActivityCategoryDTO dto)
        {
            return base.Create<ActivityCategoryDTO, ActivityCategory>(dto
                    , _IActivityCategoryRepository
                    , dtoAction =>{});
        }

        public List<DTO.ActivityCategoryDTO> Update(List<DTO.ActivityCategoryDTO> dtoList)
        {
            return base.Update<ActivityCategoryDTO, List<ActivityCategoryDTO>, ActivityCategory>(dtoList
                    , _IActivityCategoryRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        entity.Name = dto.Name;
                        entity.Desc = dto.Desc;
                        entity.ModifiedBy = dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                        entity.Version = dto.Version;
                    });
        }

        public void Delete(List<DTO.ActivityCategoryDTO> dtoList)
        {
            base.Update<ActivityCategoryDTO, List<ActivityCategoryDTO>, ActivityCategory>(dtoList
                , _IActivityCategoryRepository
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
