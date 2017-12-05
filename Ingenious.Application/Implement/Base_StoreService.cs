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
    public class Base_StoreService : ApplicationService, IBase_StoreService 
    {
        private readonly IBase_StoreRepository _IBase_StoreRepository;
        private readonly IF_UserRepository _IF_UserRepository;
        private readonly IF_UserDetailRepository _IF_UserDetailRepository;
        public Base_StoreService(IRepositoryContext context,
            IBase_StoreRepository iBase_StoreRepository,
            IF_UserRepository iF_UserRepository,
            IF_UserDetailRepository iF_UserDetailRepository)
            : base(context)
        {
            this._IBase_StoreRepository = iBase_StoreRepository;
            this._IF_UserRepository = iF_UserRepository;
            this._IF_UserDetailRepository = iF_UserDetailRepository;
        }

        /// <summary>
        /// 绑定店员到店铺
        /// </summary>
        /// <param name="storeId">店铺Id</param>
        /// <param name="userId">店员Id</param>
        /// <returns></returns>
        public bool BindStore(string storeCode, string userCode)
        {
            return false;// this._IBase_StoreRepository.BindStore(storeCode, userCode);
        }
        /// <summary>
        /// 根据导购工号获取店铺
        /// </summary>
        /// <param name="userCode">导购工号</param>
        /// <returns>Base_StoreDTO</returns>
        public Base_StoreDTO GetStoreByClerkId(string userCode)
        {
            var store = new Base_StoreDTO(); //this._IBase_StoreRepository.GetStoreByClerkId(userCode);
            //return Mapper.Map<Base_Store, Base_StoreDTO>(store);
            return store;
        }

        public Base_StoreDTO GetStoreByName(string name)
        {
            var user = this._IBase_StoreRepository.Data.Where(item => item.StoreName.ToLower().Equals(name.ToLower())).FirstOrDefault();
            if (user == null)
                return null;
            return Mapper.Map<Base_Store, Base_StoreDTO>(user);
        }

        public Base_StoreDTO GetStoreByEnglishName(string englishName)
        {
            //var store = this._IBase_StoreRepository.Data.Where(item => item.EnglishName.ToLower().Equals(englishName.ToLower())).FirstOrDefault();
            //if (store == null)
            //    return null;
            //return Mapper.Map<Base_Store, Base_StoreDTO>(store);

            return new Base_StoreDTO();
        }

        public List<F_UserDetailDTO> GetClerks(string storeCode)
        {
            var list = new List<F_UserDetailDTO>();
            //this._IBase_StoreRepository.GetClerks(storeCode).ToList().ForEach(item =>
            //    list.Add(Mapper.Map<F_UserDetail, F_UserDetailDTO>(item))
            //   );
            return list;
        }

        /// <summary>
        /// 根据店铺编码（Code）、身份证号码、手机号码、营业执照号码查询店铺
        /// </summary>
        /// <param name="code">店铺编码（Code）、身份证号码、手机号码、营业执照号码</param>
        /// <returns></returns>
        public List<Base_StoreDTO> GetStoreByCode(string code)
        {
            ISpecification<Base_Store> spec = Specification<Base_Store>.Eval(item => true);
            spec = new AndSpecification<Base_Store>(spec,
                Specification<Base_Store>.Eval(item => code == "" || item.IDNo == code));
            spec = new OrSpecification<Base_Store>(spec,
                Specification<Base_Store>.Eval(item => code == "" || item.Code == code));
            spec = new OrSpecification<Base_Store>(spec,
                Specification<Base_Store>.Eval(item => code == "" || item.BusinessLicenseNo == code));
            var list = new List<Base_StoreDTO>();
            this._IBase_StoreRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<Base_Store, Base_StoreDTO>(item))
             );
            return list;
        }

        public DTO.Base_StoreDTO GetByKey(System.Guid id)
        {
            var user = this._IBase_StoreRepository.GetByKey(id);

            var userDTO = Mapper.Map<Base_Store, Base_StoreDTO>(user);

            return userDTO;
        }

        public List<Base_StoreDTO> GetStores(bool? isActive = null,string websiteId="",DateTime? beginDate=null,DateTime? endDate=null,string sort="code_desc")
        {
            var list = new List<Base_StoreDTO>();
            ISpecification<Base_Store> spec = Specification<Base_Store>.Eval(item => true);
            spec = new AndSpecification<Base_Store>(spec,
                Specification<Base_Store>.Eval(item =>
                isActive==null || item.IsActive == isActive.Value));

            //spec = new AndSpecification<Base_Store>(spec,
            //    Specification<Base_Store>.Eval(item =>
            //    websiteId == "" || item.WebsiteId.ToString().ToLower() == websiteId.ToString().ToLower()));

            //spec = new AndSpecification<Base_Store>(spec,
            //    Specification<Base_Store>.Eval(item =>
            //    beginDate == null || item.BeginDate >= beginDate.Value ));

            //spec = new AndSpecification<Base_Store>(spec,
            //    Specification<Base_Store>.Eval(item =>
            //    endDate == null || item.EndDate < endDate.Value));

            this._IBase_StoreRepository.GetAll(spec, sort);

            return list;
        }

        public Base_StoreDTO Create(Base_StoreDTO dto)
        {
            return base.F_Create<Base_StoreDTO, Base_Store>(dto
                , _IBase_StoreRepository
                , dtoAction => { }); 
        }

        public List<Base_StoreDTO> Update(System.Collections.Generic.List<Base_StoreDTO> dtoList)
        {
            return base.F_Update<Base_StoreDTO, List<Base_StoreDTO>, Base_Store>(dtoList
                , _IBase_StoreRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.Brand = dto.Brand;
                    entity.BusinessLicenseImg = dto.BusinessLicenseImg;
                    entity.BusinessLicenseNo = dto.BusinessLicenseNo;
                    entity.City = dto.City;
                    entity.Code = dto.Code;
                    entity.Country = dto.Country;
                    entity.IDNo = dto.IDNo;
                    entity.Industry = dto.Industry;
                    entity.Mall = dto.Mall;
                    entity.Province = dto.Province;
                    entity.Area = dto.Area;
                    entity.StoreImg = dto.StoreImg;
                    entity.StoreName = dto.StoreName;

                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<Base_StoreDTO> dtoList)
        {
            base.F_Update<Base_StoreDTO, List<Base_StoreDTO>, Base_Store>(dtoList
                , _IBase_StoreRepository
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
