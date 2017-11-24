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
    public class Base_FactoryService : ApplicationService, IBase_FactoryService 
    {
        private readonly IBase_FactoryRepository _IBase_FactoryRepository;
        private readonly IF_UserRepository _IF_UserRepository;
        private readonly IF_UserDetailRepository _IF_UserDetailRepository;
        public Base_FactoryService(IRepositoryContext context,
            IBase_FactoryRepository iBase_FactoryRepository,
            IF_UserRepository iF_UserRepository,
            IF_UserDetailRepository iF_UserDetailRepository)
            : base(context)
        {
            this._IBase_FactoryRepository = iBase_FactoryRepository;
            this._IF_UserRepository = iF_UserRepository;
            this._IF_UserDetailRepository = iF_UserDetailRepository;
        }

        /// <summary>
        /// 根据身份证号码获取工厂信息
        /// </summary>
        /// <param name="idno">身份证号码</param>
        /// <returns></returns>
        public Base_FactoryDTOList GetFactoryByIDNo(string idno)
        {
            var list = new Base_FactoryDTOList();
            ISpecification<Base_Factory> spec = Specification<Base_Factory>.Eval(item => true);

            spec = new AndSpecification<Base_Factory>(spec,
                Specification<Base_Factory>.Eval(item => idno == "" || item.IDNo.Equals(idno)));
            spec = new AndSpecification<Base_Factory>(spec,
                Specification<Base_Factory>.Eval(item => idno == "" || item.BusinessLicenseNo.Equals(idno)));

            this._IBase_FactoryRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<Base_Factory, Base_FactoryDTO>(item))
                );
            return list;
        }


        /// <summary>
        /// 根据手机号码、身份证号码、营业执照编号来查询工厂
        /// </summary>
        /// <param name="code">手机号码、身份证号码、营业执照编号</param>
        /// <returns></returns>
        public Base_FactoryDTO GetFactoryByCode(string code)
        {
            ISpecification<Base_Factory> spec = Specification<Base_Factory>.Eval(item => true);
            
            spec = new AndSpecification<Base_Factory>(spec,
                Specification<Base_Factory>.Eval(item => code == "" || item.IDNo.Equals(code)));

            spec = new AndSpecification<Base_Factory>(spec,
                Specification<Base_Factory>.Eval(item => code == "" || item.BusinessLicenseNo.Equals(code)));

            return Mapper.Map<Base_Factory, Base_FactoryDTO>(this._IBase_FactoryRepository.GetAll(spec).FirstOrDefault());
        }

        public DTO.Base_FactoryDTO GetByKey(System.Guid id)
        {
            var user = this._IBase_FactoryRepository.GetByKey(id);

            var userDTO = Mapper.Map<Base_Factory, Base_FactoryDTO>(user);

            return userDTO;
        }

        public Base_FactoryDTO Create(Base_FactoryDTO dto)
        {
            return base.F_Create<Base_FactoryDTO, Base_Factory>(dto
                , _IBase_FactoryRepository
                , dtoAction => { }); 
        }

        public List<Base_FactoryDTO> Update(System.Collections.Generic.List<Base_FactoryDTO> dtoList)
        {
            return base.F_Update<Base_FactoryDTO, List<Base_FactoryDTO>, Base_Factory>(dtoList
                , _IBase_FactoryRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.Area = dto.Area;
                    entity.BusinessLicenseImg = dto.BusinessLicenseImg;
                    entity.BusinessLicenseNo = dto.BusinessLicenseNo;
                    entity.City = dto.City;
                    entity.Country = dto.Country;
                    entity.EntityName = dto.EntityName;
                    entity.IDNo = dto.IDNo;
                    entity.Industry = dto.Industry;
                    
                    entity.Province = dto.Province;
                    entity.Street = dto.Street;
                    entity.SubEntity = dto.SubEntity;
                    entity.TotalAmount = dto.TotalAmount;

                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<Base_FactoryDTO> dtoList)
        {
            base.F_Update<Base_FactoryDTO, List<Base_FactoryDTO>, Base_Factory>(dtoList
                , _IBase_FactoryRepository
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
