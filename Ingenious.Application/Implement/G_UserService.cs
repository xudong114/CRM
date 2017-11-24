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
    public class G_UserService : ApplicationService, IG_UserService
    {
        private readonly IG_UserRepository _IG_UserRepository;
        private readonly IG_UserDetailService _IG_UserDetailService;
        public G_UserService(IRepositoryContext context,
            IG_UserRepository iG_UserRepository,
            IG_UserDetailService iG_UserDetailService)
            : base(context)
        {
            this._IG_UserRepository = iG_UserRepository;
            this._IG_UserDetailService = iG_UserDetailService;
        }

        public G_UserDTO Login(G_UserDTO user)
        {
            user.Password = user.Password.ToMD5String();
            var model = Mapper.Map<G_User, G_UserDTO>(
                    this._IG_UserRepository.Login(Mapper.Map<G_UserDTO, G_User>(user))
                 );
            if (model != null)
            {
                model.G_UserDetail = this._IG_UserDetailService.GetUserDetailByUserId(model.Id);
            }
            return model;
        }

        public G_UserDTO GetUserByUserName(string userName)
        {
            var user = this._IG_UserRepository.Data.Where(item => item.UserName.ToLower().Equals(userName.ToLower())).FirstOrDefault();
            if (user == null)
                return null;
            var model = Mapper.Map<G_User, G_UserDTO>(user);
            model.G_UserDetail = this._IG_UserDetailService.GetUserDetailByUserId(model.Id);

            return model;
        }

        /// <summary>
        /// 根据推广码获取金融客服
        /// </summary>
        /// <param name="referenceCode">推广码</param>
        /// <returns></returns>
        public G_UserDTO GetGojiajuClerkByReferenceCode(string referenceCode)
        {
            var user = this._IG_UserRepository.GetUserByUserName(referenceCode);
            if (user == null)
                return null;
            var model = Mapper.Map<G_User, G_UserDTO>(user);
            
            ISpecification<G_User> spec = Specification<G_User>.Eval(item => true);
            spec = new AndSpecification<G_User>(spec,
                Specification<G_User>.Eval(item => item.G_EntityId == user.G_EntityId));
            spec = new AndSpecification<G_User>(spec,
                Specification<G_User>.Eval(item => item.UserType == G_UserTypeEnum.CS));

            var referenceUser = Mapper.Map<G_User, G_UserDTO>(this._IG_UserRepository.GetAll(spec).FirstOrDefault());
            if(referenceUser!=null)
            {
                referenceUser.G_UserDetail = this._IG_UserDetailService.GetUserDetailByUserId(referenceUser.Id);
            }
            return referenceUser;
        }

        /// <summary>
        /// 根据推广码获取金融经理
        /// </summary>
        /// <param name="entityId">机构标识</param>
        /// <returns></returns>
        public G_UserDTO GetGojiajuManagerByEntityId(Guid entityId)
        {
            ISpecification<G_User> spec = Specification<G_User>.Eval(item => true);
            spec = new AndSpecification<G_User>(spec,
                Specification<G_User>.Eval(item => item.G_EntityId == entityId));
            spec = new AndSpecification<G_User>(spec,
                Specification<G_User>.Eval(item => item.UserType == G_UserTypeEnum.CL));

            var referenceUser = Mapper.Map<G_User, G_UserDTO>(this._IG_UserRepository.GetAll(spec).FirstOrDefault());
            if (referenceUser != null)
            {
                referenceUser.G_UserDetail = this._IG_UserDetailService.GetUserDetailByUserId(referenceUser.Id);
            }
            return referenceUser;
        }

        public G_UserDTOList GetUsers(bool? isActive = true, string keywords = "", G_UserTypeEnum? userType = null, string sort = "createddate_desc")
        {
            var userDTOList = new G_UserDTOList();

            ISpecification<G_User> spec = Specification<G_User>.Eval(user => true);
            spec = new AndSpecification<G_User>(spec,
                Specification<G_User>.Eval(user => isActive == null || user.IsActive == isActive.Value));
            spec = new AndSpecification<G_User>(spec,
                Specification<G_User>.Eval(user => (keywords == "") || user.UserName.Contains(keywords)));

            spec = new AndSpecification<G_User>(spec,
            Specification<G_User>.Eval(user => (userType == null) || user.UserType == userType));


            this._IG_UserRepository.GetAll(spec, sort).ToList().ForEach(item =>
                userDTOList.Add(Mapper.Map<G_User, G_UserDTO>(item)));
           // this.F_AppendUserInfo(userDTOList, this._IG_UserRepository.Data);
            foreach(var item in userDTOList)
            {
                item.G_UserDetail = this._IG_UserDetailService.GetUserDetailByUserId(item.Id);
            }
            return userDTOList;
        }

        public DTO.G_UserDTO GetByKey(System.Guid id)
        {
            var user = this._IG_UserRepository.GetByKey(id);

            var userDTO = Mapper.Map<G_User, G_UserDTO>(user);

            return userDTO;
        }

        public G_UserDTO Create(G_UserDTO dto)
        {
            var user = base.F_Create<G_UserDTO, G_User>(dto
                , _IG_UserRepository
                , dtoAction => { });
            dto.G_UserDetail = dto.G_UserDetail ?? new G_UserDetailDTO()
            {
                Name = "未设置",
                PersonalPhone = dto.UserName,
                OfficePhone = dto.UserName
            };
            if (string.IsNullOrWhiteSpace(dto.G_UserDetail.Name))
            {
                dto.G_UserDetail.Name = "未设置";
            }
            var userDetails = new G_UserDetailDTO
            {
                G_UserId = user.Id,
                PersonalPhone = dto.G_UserDetail.PersonalPhone,
                Name = dto.G_UserDetail.Name,
                BankCode = dto.G_UserDetail.BankCode
            };

            var model = this._IG_UserDetailService.Create(userDetails);

            if (dto.UserType == G_UserTypeEnum.CL)
            {
                QRHelper.MakeWithLogo(model.Code);
            }

            return user;
        }

        public List<G_UserDTO> Update(System.Collections.Generic.List<G_UserDTO> dtoList)
        {
            var user = Mapper.Map<G_UserDTO, G_User>(dtoList.First());
            this._IG_UserRepository.Update(user);
            return new List<G_UserDTO>();
            //return base.F_Update<G_UserDTO, List<G_UserDTO>, G_User>(dtoList
            // , _IG_UserRepository
            // , dto => dto.Id
            // , (dto, entity) =>
            // {
            //     //entity.G_Entity = Mapper.Map<G_EntityDTO, G_Entity>(dto.G_Entity);
            //     entity.G_EntityId = dto.G_EntityId;
            //     entity.WebsiteId = dto.WebsiteId;
            //     entity.IsActive = dto.IsActive;
            //     entity.Password = dto.Password;
            //     entity.ModifiedBy = dto.ModifiedBy;
            // });
        }


        public void Delete(System.Collections.Generic.List<G_UserDTO> dtoList)
        {
            base.F_Update<G_UserDTO, List<G_UserDTO>, G_User>(dtoList
                , _IG_UserRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public G_UserDTOList GetBankUser(string bankcode = "", bool? isactive = null)
        {
            ISpecification<G_User> spec = Specification<G_User>.Eval(user => true);
            spec = new AndSpecification<G_User>(spec,
                Specification<G_User>.Eval(user => isactive == null || user.IsActive == isactive.Value));

            spec = new AndSpecification<G_User>(spec,
            Specification<G_User>.Eval(user => user.UserType == G_UserTypeEnum.BC
            || user.UserType == G_UserTypeEnum.BM));

            var list = new G_UserDTOList();
            this._IG_UserRepository.GetAll(spec).ToList().ForEach(item => list.Add(Mapper.Map<G_User, G_UserDTO>(item)));

            return list;
        }



        public G_UserDTOList GetUsers(bool? isActive = true, string keywords = "", F_UserTypeEnum? userType = null, string order = "createddate_desc")
        {
            throw new NotImplementedException();
        }
    }
}
