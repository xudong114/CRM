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
        public F_UserService(IRepositoryContext context,
            IF_UserRepository iF_UserRepository)
            : base(context)
        {
            this._IF_UserRepository = iF_UserRepository;
        }

        public F_UserDTO Login(F_UserDTO user)
        {
            //user.Password = user.Password.ToMD5String();
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
            return base.F_Create<F_UserDTO, F_User>(dto
                , _IF_UserRepository
                , dtoAction => { }); 
        }

        public List<F_UserDTO> Update(System.Collections.Generic.List<F_UserDTO> dtoList)
        {
            return base.F_Update<F_UserDTO, List<F_UserDTO>, F_User>(dtoList
                , _IF_UserRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.Password = dto.Password;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<F_UserDTO> dtoList)
        {
            base.F_Update<F_UserDTO, List<F_UserDTO>, F_User>(dtoList
                , _IF_UserRepository
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
