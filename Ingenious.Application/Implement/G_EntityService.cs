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
using Ingenious.Infrastructure.Extensions;

namespace Ingenious.Application.Implement
{
    public class G_EntityService : ApplicationService, IG_EntityService
    {
        private readonly IG_EntityRepository _IG_EntityRepository;
        private readonly IF_UserRepository _IF_UserRepository;
        private readonly IF_UserDetailRepository _IF_UserDetailRepository;
        public G_EntityService(IRepositoryContext context,
            IG_EntityRepository iG_EntityRepository,
            IF_UserRepository iF_UserRepository,
            IF_UserDetailRepository iF_UserDetailRepository)
            : base(context)
        {
            this._IG_EntityRepository = iG_EntityRepository;
            this._IF_UserRepository = iF_UserRepository;
            this._IF_UserDetailRepository = iF_UserDetailRepository;
        }

        /// <summary>
        /// 获取佳居贷经销商分页数据
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页显示记录条数</param>
        /// <param name="keyword">关键字： 机构负责人姓名、机构编码、联系店铺（手机、办公电话）</param>
        /// <param name="province">所在省份</param>
        /// <param name="city">所在城市</param>
        /// <param name="country">所在县区</param>
        /// <param name="isActive">是否可用</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public G_EntityDTOListWithPagination GetAll(int pageIndex, int pageSize, string keyword = "", string province = "", string city = "", string country = "", bool? isActive = true, string sort = "createddate_desc")
        {
            ISpecification<G_Entity> spec = Specification<G_Entity>.Eval(item => true);

            spec = new AndSpecification<G_Entity>(spec,
                Specification<G_Entity>.Eval(item =>
                keyword == null || keyword == ""
                || item.Name.Contains(keyword)
                || item.Code.Contains(keyword)
                || item.PersonalPhone.Contains(keyword)
                || item.OfficePhone.Contains(keyword)
                ));
            spec = new AndSpecification<G_Entity>(spec,
                Specification<G_Entity>.Eval(item =>
                province == null || province == "" || item.Province.Contains(province)));
            spec = new AndSpecification<G_Entity>(spec,
                Specification<G_Entity>.Eval(item =>
                city == null || city == "" || item.City.Contains(city)));
            spec = new AndSpecification<G_Entity>(spec,
                Specification<G_Entity>.Eval(item =>
                country == null || country == "" || item.Country.Contains(country)));
            spec = new AndSpecification<G_Entity>(spec,
                Specification<G_Entity>.Eval(item =>
                isActive == null || isActive.HasValue || item.IsActive == isActive.Value));

            var list = new G_EntityDTOList();
            var result = this._IG_EntityRepository.GetAll(pageIndex, pageSize, spec, sort);

            int totalPages = 0;
            int totalRecords = 0;
            if (result != null)
            {
                result.Data.ForEach(item =>
                    list.Add(Mapper.Map<G_Entity, G_EntityDTO>(item))
                );
                totalPages = result.TotalPages;
                totalRecords = result.TotalRecords;
            }

            return new G_EntityDTOListWithPagination
            {
                TotalRecords = totalRecords,
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Data = list
            };
        }

        public DTO.G_EntityDTO GetByKey(System.Guid id)
        {
            var user = this._IG_EntityRepository.GetByKey(id);

            var userDTO = Mapper.Map<G_Entity, G_EntityDTO>(user);

            return userDTO;
        }

        public G_EntityDTO Create(G_EntityDTO dto)
        {
            //int count = this._IG_EntityRepository.Data.SafeMax(item => int.Parse(item.Code));
            string code = this._IG_EntityRepository.GetMaxCode();
            
            if(string.IsNullOrWhiteSpace(code))
            {
                code = "1000";
            }else
            {
                code = (int.Parse(code) + 1).ToString();
            }
            dto.Code = code;
            return base.F_Create<G_EntityDTO, G_Entity>(dto
                , _IG_EntityRepository
                , dtoAction => { });
        }

        public List<G_EntityDTO> Update(System.Collections.Generic.List<G_EntityDTO> dtoList)
        {
            return base.F_Update<G_EntityDTO, List<G_EntityDTO>, G_Entity>(dtoList
                , _IG_EntityRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.BusinessLicenseImg = dto.BusinessLicenseImg;
                    entity.BusinessLicenseNo = dto.BusinessLicenseNo;
                    entity.City = dto.City;
                    entity.Country = dto.Country;
                    entity.EntityName = dto.EntityName;
                    entity.IDImg = dto.IDImg;
                    entity.IDNo = dto.IDNo;
                    entity.Industry = dto.Industry;
                    entity.Name = dto.Name;
                    entity.OfficePhone = dto.OfficePhone;
                    entity.PersonalPhone = dto.PersonalPhone;
                    entity.Province = dto.Province;
                    entity.Remark = dto.Remark;
                    entity.Street = dto.Street;
                    entity.UserId = dto.UserId;
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public void Delete(System.Collections.Generic.List<G_EntityDTO> dtoList)
        {
            base.F_Update<G_EntityDTO, List<G_EntityDTO>, G_Entity>(dtoList
                , _IG_EntityRepository
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
