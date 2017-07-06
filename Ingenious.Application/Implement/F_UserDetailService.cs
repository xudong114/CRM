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
    public class F_UserDetailService : ApplicationService, IF_UserDetailService 
    {
        private readonly IF_UserDetailRepository _IF_UserDetailRepository;
        public F_UserDetailService(IRepositoryContext context,
            IF_UserDetailRepository iF_UserDetailRepository)
            : base(context)
        {
            this._IF_UserDetailRepository = iF_UserDetailRepository;
        }

        public F_UserDetailDTO GetUserDetailByUserId(Guid userId)
        {
            var model = this._IF_UserDetailRepository.Data.Where(item => item.F_UserId == userId).FirstOrDefault();
            return Mapper.Map<F_UserDetail, F_UserDetailDTO>(model);
        }

        public F_UserDetailDTO GetByKey(Guid id)
        {
            var model = this._IF_UserDetailRepository.Data.Where(item => item.Id == id).FirstOrDefault();
            return Mapper.Map<F_UserDetail, F_UserDetailDTO>(model);
        }

        public F_UserDetailDTO Create(F_UserDetailDTO dto)
        {
            return base.F_Create<F_UserDetailDTO, F_UserDetail>(dto
                , _IF_UserDetailRepository
                , dtoAction => { }); 
        }

        public List<F_UserDetailDTO> Update(System.Collections.Generic.List<F_UserDetailDTO> dtoList)
        {
            return base.F_Update<F_UserDetailDTO, List<F_UserDetailDTO>, F_UserDetail>(dtoList
                , _IF_UserDetailRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<F_UserDetailDTO> dtoList)
        {
            base.F_Update<F_UserDetailDTO, List<F_UserDetailDTO>, F_UserDetail>(dtoList
                , _IF_UserDetailRepository
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
