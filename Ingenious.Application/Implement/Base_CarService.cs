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
    public class Base_CarService : ApplicationService, IBase_CarService
    {
        private readonly IBase_CarRepository _IBase_CarRepository;
        public Base_CarService(IRepositoryContext context,
            IBase_CarRepository iBase_CarRepository)
            : base(context)
        {
            this._IBase_CarRepository = iBase_CarRepository;
        }

        /// <summary>
        /// 根据身份证号码获取个人信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        public Base_CarDTOList GetCarByIDNo(string idNo)
        {
            var list = new Base_CarDTOList();
            this._IBase_CarRepository.GetCarByIDNo(idNo).ToList().ForEach(item =>
                list.Add(Mapper.Map<Base_Car, Base_CarDTO>(item))
            );
            return list;
        }

        public DTO.Base_CarDTO GetByKey(System.Guid id)
        {
            var model = this._IBase_CarRepository.GetByKey(id);

            var modelDTO = Mapper.Map<Base_Car, Base_CarDTO>(model);

            return modelDTO;
        }

        public Base_CarDTO Create(Base_CarDTO dto)
        {
            var user = base.F_Create<Base_CarDTO, Base_Car>(dto
                , _IBase_CarRepository
                , dtoAction => { });

            return user;
        }

        public List<Base_CarDTO> Update(System.Collections.Generic.List<Base_CarDTO> dtoList)
        {
            var list = new List<Base_CarDTO>();

            base.F_Update<Base_CarDTO, List<Base_CarDTO>, Base_Car>(dtoList
             , _IBase_CarRepository
             , dto => dto.Id
             , (dto, entity) =>
             {
                 entity.Brand = dto.Brand;
                 entity.IDNo = dto.IDNo;
                 entity.IsSecondHand = dto.IsSecondHand;
                 entity.LicenseImg = dto.LicenseImg;
                 entity.PurchasedDate = dto.PurchasedDate;
                 entity.Remark = dto.Remark;
                 entity.Valuation = dto.Valuation;
                 entity.VMT = dto.VMT;
                 entity.IsActive = dto.IsActive;
                 entity.ModifiedBy = dto.ModifiedBy;
             });

            return list;
        }


        public void Delete(System.Collections.Generic.List<Base_CarDTO> dtoList)
        {
            base.F_Update<Base_CarDTO, List<Base_CarDTO>, Base_Car>(dtoList
                , _IBase_CarRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


    }
}
