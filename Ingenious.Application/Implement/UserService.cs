using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using Ingenious.Infrastructure.Helper;
using Ingenious.Repositories;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ingenious.Application.Implement
{
    public class UserService : ApplicationService, IUserService 
    {
        private readonly IUserRepository _IUserRepository;
        public UserService(IRepositoryContext context,
            IUserRepository iUserRepository)
            : base(context)
        {
            this._IUserRepository = iUserRepository;
        }

        public UserDTO Login(UserDTO user)
        {
            user.Password = user.Password.ToMD5String();
            return Mapper.Map<User, UserDTO>(
                    this._IUserRepository.Login(Mapper.Map<UserDTO, User>(user))
                 );
        }

        public UserDTOList GetUsers(Guid? department, UserStatusEnum status, string keywords = "", string order = "createddate_desc")
        {
            var userDTOList = new UserDTOList();

            ISpecification<User> spec = Specification<User>.Eval(user => true);
            spec = new AndSpecification<User>(spec,
                Specification<User>.Eval(user => status == UserStatusEnum.All || user.Status == status));
            spec = new AndSpecification<User>(spec,
                Specification<User>.Eval(user => (keywords == "") || user.UserName.Contains(keywords)));
            spec = new AndSpecification<User>(spec,
                Specification<User>.Eval(user => !(department.HasValue) || user.DepartmentId == department.Value));

            this._IUserRepository.GetAll(spec).ToList().ForEach(item =>
                userDTOList.Add(Mapper.Map<User, UserDTO>(item)));

            return userDTOList;
        }

        public DTO.UserDTO GetByKey(System.Guid id)
        {
            var user = this._IUserRepository.GetByKey(id);
            
            var userDTO =  Mapper.Map<User, UserDTO>(user);

            return userDTO;
        }

        public UserDTO Create(UserDTO dto)
        {
            return base.Create<UserDTO, User>(dto
                , _IUserRepository
                , dtoAction => {});
        }

        public List<UserDTO> Update(System.Collections.Generic.List<UserDTO> dtoList)
        {
            return base.Update<UserDTO, List<UserDTO>, User>(dtoList
                , _IUserRepository
                , dto => dto.Id
                , (dto, entity) =>
                {

                });
        }

        public void Delete(System.Collections.Generic.List<UserDTO> dtoList)
        {
            base.Update<UserDTO, List<UserDTO>, User>(dtoList
            , _IUserRepository
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
