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
    public class F_StoreService : ApplicationService, IF_StoreService 
    {
        private readonly IF_StoreRepository _IF_StoreRepository;
        private readonly IF_UserRepository _IF_UserRepository;
        private readonly IF_UserDetailRepository _IF_UserDetailRepository;
        public F_StoreService(IRepositoryContext context,
            IF_StoreRepository iF_StoreRepository,
            IF_UserRepository iF_UserRepository,
            IF_UserDetailRepository iF_UserDetailRepository)
            : base(context)
        {
            this._IF_StoreRepository = iF_StoreRepository;
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
            return this._IF_StoreRepository.BindStore(storeCode, userCode);
        }
        /// <summary>
        /// 根据导购工号获取店铺
        /// </summary>
        /// <param name="userCode">导购工号</param>
        /// <returns>F_StoreDTO</returns>
        public F_StoreDTO GetStoreByClerkId(string userCode)
        {
            var store = this._IF_StoreRepository.GetStoreByClerkId(userCode);
            return Mapper.Map<F_Store, F_StoreDTO>(store);
        }

        public F_StoreDTO GetStoreByName(string name)
        {
            var user = this._IF_StoreRepository.Data.Where(item => item.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
            if (user == null)
                return null;
            return Mapper.Map<F_Store, F_StoreDTO>(user);
        }

        public F_StoreDTO GetStoreByEnglishName(string englishName)
        {
            var store = this._IF_StoreRepository.Data.Where(item => item.EnglishName.ToLower().Equals(englishName.ToLower())).FirstOrDefault();
            if (store == null)
                return null;
            return Mapper.Map<F_Store, F_StoreDTO>(store);
        }

        public List<F_UserDetailDTO> GetClerks(string storeCode)
        {
            var list = new List<F_UserDetailDTO>();
            this._IF_StoreRepository.GetClerks(storeCode).ToList().ForEach(item =>
                list.Add(Mapper.Map<F_UserDetail, F_UserDetailDTO>(item))
               );
            return list;
        }

        public F_StoreDTO GetStoreByCode(string code)
        {
            var user = this._IF_StoreRepository.Data.Where(item => item.Code.ToLower().Equals(code.ToLower())).FirstOrDefault();
            if (user == null)
                return null;
            return Mapper.Map<F_Store, F_StoreDTO>(user);
        }

        public DTO.F_StoreDTO GetByKey(System.Guid id)
        {
            var user = this._IF_StoreRepository.GetByKey(id);

            var userDTO = Mapper.Map<F_Store, F_StoreDTO>(user);

            return userDTO;
        }

        public List<F_StoreDTO> GetStores(bool? isActive = null,string websiteId="",DateTime? beginDate=null,DateTime? endDate=null,string sort="code_desc")
        {
            var list = new List<F_StoreDTO>();
            ISpecification<F_Store> spec = Specification<F_Store>.Eval(item => true);
            spec = new AndSpecification<F_Store>(spec,
                Specification<F_Store>.Eval(item =>
                isActive==null || item.IsActive == isActive.Value));

            spec = new AndSpecification<F_Store>(spec,
                Specification<F_Store>.Eval(item =>
                websiteId == "" || item.WebsiteId.ToString().ToLower() == websiteId.ToString().ToLower()));

            spec = new AndSpecification<F_Store>(spec,
                Specification<F_Store>.Eval(item =>
                beginDate == null || item.BeginDate >= beginDate.Value ));

            spec = new AndSpecification<F_Store>(spec,
                Specification<F_Store>.Eval(item =>
                endDate == null || item.EndDate < endDate.Value));

            this._IF_StoreRepository.GetAll(spec, sort);

            return list;
        }

        public F_StoreDTO Create(F_StoreDTO dto)
        {
            return base.F_Create<F_StoreDTO, F_Store>(dto
                , _IF_StoreRepository
                , dtoAction => { }); 
        }

        public List<F_StoreDTO> Update(System.Collections.Generic.List<F_StoreDTO> dtoList)
        {
            return base.F_Update<F_StoreDTO, List<F_StoreDTO>, F_Store>(dtoList
                , _IF_StoreRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<F_StoreDTO> dtoList)
        {
            base.F_Update<F_StoreDTO, List<F_StoreDTO>, F_Store>(dtoList
                , _IF_StoreRepository
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
