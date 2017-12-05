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
    public class Base_ProfileService : ApplicationService, IBase_ProfileService
    {
        private readonly IBase_ProfileRepository _IBase_ProfileRepository;
        public Base_ProfileService(IRepositoryContext context,
            IBase_ProfileRepository iBase_ProfileRepository)
            : base(context)
        {
            this._IBase_ProfileRepository = iBase_ProfileRepository;
        }

        /// <summary>
        /// 根据身份证号码获取个人信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        public Base_ProfileDTO GetProfileByIDNo(string idNo)
        {
            var model = this._IBase_ProfileRepository.GetProfileByIDNo(idNo);
            return Mapper.Map<Base_Profile, Base_ProfileDTO>(model);
        }

        /// <summary>
        /// 获取个人分页数据
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="idno">身份证号码</param>
        /// <param name="personalPhone">手机号码</param>
        /// <param name="name">姓名</param>
        /// <param name="nativeplace">籍贯</param>
        /// <param name="wechat">微信号</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="officePhone">办公电话</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public Base_ProfileDTOListWithPagination GetAll(int pageIndex, int pageSize, string idno = "", string personalPhone = "", string name = "", string nativeplace = "", string wechat = "", string email = "", string officePhone = "", string sort = "createddate_desc")
        {
            ISpecification<Base_Profile> spec = Specification<Base_Profile>.Eval(item => true);

            //身份证号码
            spec = new AndSpecification<Base_Profile>(spec,
                Specification<Base_Profile>.Eval(item =>
                idno == null || idno == "" || item.IDNo.Equals(idno)));
            //手机号码
            spec = new AndSpecification<Base_Profile>(spec,
                Specification<Base_Profile>.Eval(item =>
                personalPhone == null || personalPhone == "" || item.PersonalPhone.Equals(personalPhone)));
            //姓名
            spec = new AndSpecification<Base_Profile>(spec,
                Specification<Base_Profile>.Eval(item =>
                name == null || name == "" || item.Name.Equals(name)));
            //籍贯
            spec = new AndSpecification<Base_Profile>(spec,
                Specification<Base_Profile>.Eval(item =>
                nativeplace == null || nativeplace == "" || item.NativePlace.Equals(nativeplace)));
            //微信
            spec = new AndSpecification<Base_Profile>(spec,
                Specification<Base_Profile>.Eval(item =>
                wechat == null || wechat == "" || item.Wechat.Equals(wechat)));
            //电子邮箱
            spec = new AndSpecification<Base_Profile>(spec,
                Specification<Base_Profile>.Eval(item =>
                email == null || email == "" || item.Email.Equals(email)));
            //办公电话
            spec = new AndSpecification<Base_Profile>(spec,
                Specification<Base_Profile>.Eval(item =>
                officePhone == null || officePhone == "" || item.OfficePhone.Equals(officePhone)));
            //未删除
            spec = new AndSpecification<Base_Profile>(spec,
                Specification<Base_Profile>.Eval(item =>item.IsActive));


            var list = new Base_ProfileDTOList();

            var result = this._IBase_ProfileRepository.GetAll(pageIndex, pageSize, spec, sort);

            int totalPages = 0;
            int totalRecords = 0;
            if (result != null)
            {
                result.Data.ForEach(item =>
                    list.Add(Mapper.Map<Base_Profile, Base_ProfileDTO>(item))
                );
                totalPages = result.TotalPages;
                totalRecords = result.TotalRecords;
            }

            return new Base_ProfileDTOListWithPagination
            {
                TotalRecords = totalRecords,
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Data = list,
                Rows = list,
            };
        }


        public DTO.Base_ProfileDTO GetByKey(System.Guid id)
        {
            var user = this._IBase_ProfileRepository.GetByKey(id);

            var userDTO = Mapper.Map<Base_Profile, Base_ProfileDTO>(user);

            return userDTO;
        }

        public Base_ProfileDTO Create(Base_ProfileDTO dto)
        {
            var user = base.F_Create<Base_ProfileDTO, Base_Profile>(dto
                , _IBase_ProfileRepository
                , dtoAction => { });

            return user;
        }

        public List<Base_ProfileDTO> Update(System.Collections.Generic.List<Base_ProfileDTO> dtoList)
        {
            var list = new List<Base_ProfileDTO>();

            base.F_Update<Base_ProfileDTO, List<Base_ProfileDTO>, Base_Profile>(dtoList
             , _IBase_ProfileRepository
             , dto => dto.Id
             , (dto, entity) =>
             {
                 entity.Code = dto.Code;
                 entity.Email = dto.Email;
                 entity.Gender = dto.Gender;
                 entity.IDImg = dto.IDImg;
                 entity.IDNo = dto.IDNo;
                 entity.Name = dto.Name;
                 entity.NativePlace = dto.NativePlace;
                 entity.OfficePhone = dto.OfficePhone;
                 entity.PersonalPhone = dto.PersonalPhone;
                 entity.QQ = dto.QQ;
                 entity.Remark = dto.Remark;
                 entity.Wechat = dto.Wechat;
                 entity.Weibo = dto.Wechat;
                 entity.IsActive = dto.IsActive;
                 entity.ModifiedBy = dto.ModifiedBy;
             });

            return list;
        }


        public void Delete(System.Collections.Generic.List<Base_ProfileDTO> dtoList)
        {
            base.F_Update<Base_ProfileDTO, List<Base_ProfileDTO>, Base_Profile>(dtoList
                , _IBase_ProfileRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


    }
}
