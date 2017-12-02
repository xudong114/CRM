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
    public class Base_RealestateService : ApplicationService, IBase_RealestateService
    {
        private readonly IBase_RealestateRepository _IBase_RealestateRepository;
        public Base_RealestateService(IRepositoryContext context,
            IBase_RealestateRepository iBase_RealestateRepository)
            : base(context)
        {
            this._IBase_RealestateRepository = iBase_RealestateRepository;
        }

        /// <summary>
        /// 根据身份证号码获取个人信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        public Base_RealestateDTOList GetRealestateByIDNo(string idNo)
        {
            var list = new Base_RealestateDTOList();
            this._IBase_RealestateRepository.GetRealestateByIDNo(idNo).ToList().ForEach(item =>
                list.Add(Mapper.Map<Base_Realestate, Base_RealestateDTO>(item))
            );
            return list;
        }

        public DTO.Base_RealestateDTO GetByKey(System.Guid id)
        {
            var model = this._IBase_RealestateRepository.GetByKey(id);

            var modelDTO = Mapper.Map<Base_Realestate, Base_RealestateDTO>(model);

            return modelDTO;
        }

        public Base_RealestateDTO Create(Base_RealestateDTO dto)
        {
            var user = base.F_Create<Base_RealestateDTO, Base_Realestate>(dto
                , _IBase_RealestateRepository
                , dtoAction => { });

            return user;
        }

        public List<Base_RealestateDTO> Update(System.Collections.Generic.List<Base_RealestateDTO> dtoList)
        {
            var list = new List<Base_RealestateDTO>();

            base.F_Update<Base_RealestateDTO, List<Base_RealestateDTO>, Base_Realestate>(dtoList
             , _IBase_RealestateRepository
             , dto => dto.Id
             , (dto, entity) =>
             {
                 entity.Area = dto.Area;
                 entity.Category = dto.Category;
                 entity.City = dto.City;
                 entity.Community = dto.Community;
                 entity.Country = dto.Country;
                 entity.IDNo = dto.IDNo;
                 entity.LicenseImg = dto.LicenseImg;
                 entity.Province = dto.Province;
                 entity.Remark = dto.Remark;
                 entity.Street = dto.Street;
                 entity.Valuation = dto.Valuation;
                 entity.ModifiedBy = dto.ModifiedBy;
             });

            return list;
        }

        public void Delete(System.Collections.Generic.List<Base_RealestateDTO> dtoList)
        {
            base.F_Update<Base_RealestateDTO, List<Base_RealestateDTO>, Base_Realestate>(dtoList
                , _IBase_RealestateRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


    }
}
