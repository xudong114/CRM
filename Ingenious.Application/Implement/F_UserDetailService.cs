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
    public class F_UserDetailService : ApplicationService, IF_UserDetailService 
    {
        private readonly IF_UserDetailRepository _IF_UserDetailRepository;
        private readonly IF_BankService _IF_BankService;
        public F_UserDetailService(IRepositoryContext context,
            IF_UserDetailRepository iF_UserDetailRepository,
            IF_BankService iF_BankService)
            : base(context)
        {
            this._IF_UserDetailRepository = iF_UserDetailRepository;
            this._IF_BankService = iF_BankService;
        }

        public F_UserDetailDTO GetUserDetailByUserId(Guid userId)
        {
            var model = this._IF_UserDetailRepository.Data.Where(item => item.F_UserId == userId).FirstOrDefault();
            
            var userDetail = Mapper.Map<F_UserDetail, F_UserDetailDTO>(model);
            if(userDetail!=null)
            {
                var bank = this._IF_BankService.GetBankByUserId(userDetail.Id);
                if(bank!=null)
                {
                    userDetail.BankName = bank.Name;
                }
            }
            return userDetail;
        }

        public F_UserDetailDTO GetByKey(Guid id)
        {
            var model = this._IF_UserDetailRepository.Data.Where(item => item.Id == id).FirstOrDefault();
            return Mapper.Map<F_UserDetail, F_UserDetailDTO>(model);
        }

        public F_UserDetailDTO GetUserDetailByCode(string code)
        {
            var userdetail = this._IF_UserDetailRepository.GetUserDetailByCode(code);
            return Mapper.Map<F_UserDetail, F_UserDetailDTO>(userdetail);
        }

        public List<F_UserDetailDTO> GetUserDetailByBankCode(string bankCode)
        {
            var list = new List<F_UserDetailDTO>();
            this._IF_UserDetailRepository.Data.Where(item => item.BankCode.Equals(bankCode) && item.F_User.IsActive)
                .ToList()
                .ForEach(item =>
                    list.Add(Mapper.Map<F_UserDetail, F_UserDetailDTO>(item))
                );
            return list;
        }

        public List<F_UserDetailDTO> GetUserByBankCode(string bankCode)
        {
            var list = new List<F_UserDetailDTO>();
            this._IF_UserDetailRepository.Data.Where(item => item.BankCode.Equals(bankCode) && item.IsActive)
                .ToList()
                .ForEach(item =>
                    list.Add(Mapper.Map<F_UserDetail, F_UserDetailDTO>(item))
                );
            return list;
        }

        public F_UserDetailDTO Create(F_UserDetailDTO dto)
        {
           var maxCode = this._IF_UserDetailRepository.Data.Where(item => item.Code != null && item.Code != "")
                .Max(item => item.Code);
           var newCode = string.IsNullOrWhiteSpace(maxCode) ? 0 : int.Parse(maxCode);
            if (newCode == 0)
            {
                newCode = 10000;
            }
            else
            {
                newCode += 1;
            }
            dto.Code = string.Format("00{0}", newCode);
            return base.F_Create<F_UserDetailDTO, F_UserDetail>(dto
                , _IF_UserDetailRepository
                , dtoAction => { }); 
        }

        public List<F_UserDetailDTO> Update(System.Collections.Generic.List<F_UserDetailDTO> dtoList)
        {
            return base.F_Update<F_UserDetailDTO, List<F_UserDetailDTO>, F_UserDetail>(dtoList
                , _IF_UserDetailRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.Birthdate = dto.Birthdate;
                    entity.BankCode = dto.BankCode;
                    entity.Email = dto.Email;
                    entity.Face = dto.Face;
                    entity.Gender = dto.Gender;
                    entity.ModifiedBy = dto.ModifiedBy;
                    entity.Name = dto.Name;
                    entity.NickName = dto.NickName;
                    entity.OfficePhone = dto.OfficePhone;
                    entity.PersonalPhone = dto.PersonalPhone;
                    entity.QQ = dto.QQ;
                    entity.Title = dto.Title;
                    entity.Wechat = dto.Wechat;
                    entity.Province = dto.Province;
                    entity.City = dto.City;
                    entity.Country = dto.Country;
                    entity.Street = dto.Street;
                    entity.IDNo = dto.IDNo;
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<F_UserDetailDTO> dtoList)
        {
            base.F_Update<F_UserDetailDTO, List<F_UserDetailDTO>, F_UserDetail>(dtoList
                , _IF_UserDetailRepository
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
