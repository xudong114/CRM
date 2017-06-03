using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
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
    public class UserDetailService : ApplicationService, IUserDetailService
    {
        private readonly IUserDetailRepository _IUserDetailRepository;
        public UserDetailService(IRepositoryContext context,
    IUserDetailRepository iUserDetailRepository)
            : base(context)
        {
            this._IUserDetailRepository = iUserDetailRepository;
        }

        public DTO.UserDetailDTO GetByKey(Guid id)
        {
            var user = this._IUserDetailRepository.GetByKey(id);

            var userDTO = Mapper.Map<UserDetail, UserDetailDTO>(user);

            //if(userDTO.UserDetailDTO==null)
            //    userDTO.UserDetailDTO = new UserDetailDTO();

            return userDTO;
        }

        public DTO.UserDetailDTO Create(DTO.UserDetailDTO dto)
        {
            return base.Create<UserDetailDTO, UserDetail>(dto
                , _IUserDetailRepository
                , dtoAction =>{});
        }

        public List<DTO.UserDetailDTO> Update(List<DTO.UserDetailDTO> dtoList)
        {
            return base.Update<UserDetailDTO, List<UserDetailDTO>, UserDetail>(dtoList
                , _IUserDetailRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.Birthdate = dto.Birthdate;
                    entity.Email = dto.Email;
                    entity.Gender = dto.Gender;
                    entity.Hiredate = dto.Hiredate;
                    entity.Logo = dto.Logo;
                    if(!string.IsNullOrWhiteSpace(dto.Name))
                    {
                        entity.Name = dto.Name;
                    }
                    entity.OfficePhone = dto.OfficePhone;
                    entity.PersonalPhone = dto.PersonalPhone;
                    entity.QQ = dto.QQ;
                    entity.Termdate = dto.Termdate;
                    entity.Title = dto.Title;
                    entity.Wechat = dto.Wechat;
                });
        }

        public void Delete(List<DTO.UserDetailDTO> dtoList)
        {
            base.Update<UserDetailDTO, List<UserDetailDTO>, UserDetail>(dtoList
            , _IUserDetailRepository
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
