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
    public class F_UserService : ApplicationService, IF_UserService 
    {
        private readonly IF_UserRepository _IF_UserRepository;
        private readonly IF_UserDetailService _IF_UserDetailService;
        public F_UserService(IRepositoryContext context,
            IF_UserRepository iF_UserRepository, 
            IF_UserDetailService iF_UserDetailService)
            : base(context)
        {
            this._IF_UserRepository = iF_UserRepository;
            this._IF_UserDetailService = iF_UserDetailService;
        }

        public F_UserDTO Login(F_UserDTO user)
        {
            user.Password = user.Password.ToMD5String();
            return Mapper.Map<F_User, F_UserDTO>(
                    this._IF_UserRepository.Login(Mapper.Map<F_UserDTO, F_User>(user))
                 );
        }

        public F_UserDTO GetUserByUserName(string userName)
        {
            var user = this._IF_UserRepository.Data.Where(item => item.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault();
            if (user == null)
                return null;
            return Mapper.Map<F_User, F_UserDTO>(user);
        }

        public F_UserDTOList GetUsers( bool? isActive = true, string keywords = "", string order = "createddate_desc")
        {
            var userDTOList = new F_UserDTOList();

            ISpecification<F_User> spec = Specification<F_User>.Eval(user => true);
            spec = new AndSpecification<F_User>(spec,
                Specification<F_User>.Eval(user => isActive == null || user.IsActive == isActive.Value));
            spec = new AndSpecification<F_User>(spec,
                Specification<F_User>.Eval(user => (keywords == "") || user.UserName.Contains(keywords)));

            this._IF_UserRepository.GetAll(spec).ToList().ForEach(item =>
                userDTOList.Add(Mapper.Map<F_User, F_UserDTO>(item)));
            this.F_AppendUserInfo(userDTOList, this._IF_UserRepository.Data);
            return userDTOList;
        }

        public DTO.F_UserDTO GetByKey(System.Guid id)
        {
            var user = this._IF_UserRepository.GetByKey(id);

            var userDTO = Mapper.Map<F_User, F_UserDTO>(user);

            return userDTO;
        }

        public F_UserDTO Create(F_UserDTO dto)
        {
           
            var user= base.F_Create<F_UserDTO, F_User>(dto
                , _IF_UserRepository
                , dtoAction => { });
            dto.F_UserDetail = dto.F_UserDetail ?? new F_UserDetailDTO() { 
                Name = "未设置",
                PersonalPhone = dto.UserName,
            };
            
            var userDetails = new F_UserDetailDTO { 
                F_UserId = user.Id,
                PersonalPhone = dto.F_UserDetail.PersonalPhone,
                Name = dto.F_UserDetail.Name,
                BankCode = dto.F_UserDetail.BankCode
            };

            var model = this._IF_UserDetailService.Create(userDetails);

            if(dto.UserType== F_UserTypeEnum.CL)
            {
                QRHelper.MakeWithLogo(model.Code);
            }

            return user;
        }

        public List<F_UserDTO> Update(System.Collections.Generic.List<F_UserDTO> dtoList)
        {
            var list = new List<F_UserDTO>();
           
                base.F_Update<F_UserDTO, List<F_UserDTO>, F_User>(dtoList
                 , _IF_UserRepository
                 , dto => dto.Id
                 , (dto, entity) =>
                 {
                     entity.WebsiteId = dto.WebsiteId;
                     entity.IsActive = dto.IsActive;
                     entity.Password = dto.Password;
                     entity.ModifiedBy = dto.ModifiedBy;
                 });
           
            return list;
        }


        public void Delete(System.Collections.Generic.List<F_UserDTO> dtoList)
        {
            base.F_Update<F_UserDTO, List<F_UserDTO>, F_User>(dtoList
                , _IF_UserRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

    }
}
