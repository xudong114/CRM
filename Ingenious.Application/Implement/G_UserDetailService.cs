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
    public class G_UserDetailService : ApplicationService, IG_UserDetailService
    {
        private readonly IG_UserDetailRepository _IG_UserDetailRepository;
        private readonly IG_BankService _IG_BankService;
        public G_UserDetailService(IRepositoryContext context,
            IG_UserDetailRepository iG_UserDetailRepository,
            IG_BankService iG_BankService)
            : base(context)
        {
            this._IG_UserDetailRepository = iG_UserDetailRepository;
            this._IG_BankService = iG_BankService;
        }

        public G_UserDetailDTO GetUserDetailByUserId(Guid userId)
        {
            var model = this._IG_UserDetailRepository.Data.Where(item => item.G_UserId == userId).FirstOrDefault();

            var userDetail = Mapper.Map<G_UserDetail, G_UserDetailDTO>(model);
            if (userDetail != null)
            {
                if (!string.IsNullOrWhiteSpace(userDetail.BankCode))
                {
                    var bank = this._IG_BankService.GetBanks(userDetail.BankCode).FirstOrDefault();
                    if (bank != null)
                    {
                        userDetail.BankName = bank.Name;
                    }
                }
            }
            return userDetail;
        }

        public G_UserDetailDTO GetByKey(Guid id)
        {
            var model = this._IG_UserDetailRepository.Data.Where(item => item.Id == id).FirstOrDefault();
            return Mapper.Map<G_UserDetail, G_UserDetailDTO>(model);
        }

        public G_UserDetailDTO GetUserDetailByCode(string code)
        {
            var userdetail = this._IG_UserDetailRepository.GetUserDetailByCode(code);
            return Mapper.Map<G_UserDetail, G_UserDetailDTO>(userdetail);
        }

        public List<G_UserDetailDTO> GetUserDetailByBankCode(string bankCode)
        {
            var list = new List<G_UserDetailDTO>();
            this._IG_UserDetailRepository.Data.Where(item => item.BankCode.Equals(bankCode) && item.G_User.IsActive)
                .ToList()
                .ForEach(item =>
                    list.Add(Mapper.Map<G_UserDetail, G_UserDetailDTO>(item))
                );
            return list;
        }

        public List<G_UserDetailDTO> GetUserByBankCode(string bankCode)
        {
            var list = new List<G_UserDetailDTO>();
            this._IG_UserDetailRepository.Data.Where(item => item.BankCode.Equals(bankCode) && item.IsActive)
                .ToList()
                .ForEach(item =>
                    list.Add(Mapper.Map<G_UserDetail, G_UserDetailDTO>(item))
                );
            return list;
        }

        public G_UserDetailDTO Create(G_UserDetailDTO dto)
        {
            var maxCode = this._IG_UserDetailRepository.Data.Where(item => item.Code != null && item.Code != "")
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
            return base.F_Create<G_UserDetailDTO, G_UserDetail>(dto
                , _IG_UserDetailRepository
                , dtoAction => { });
        }

        public List<G_UserDetailDTO> Update(System.Collections.Generic.List<G_UserDetailDTO> dtoList)
        {
            return base.F_Update<G_UserDetailDTO, List<G_UserDetailDTO>, G_UserDetail>(dtoList
                , _IG_UserDetailRepository
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


        public void Delete(System.Collections.Generic.List<G_UserDetailDTO> dtoList)
        {
            base.F_Update<G_UserDetailDTO, List<G_UserDetailDTO>, G_UserDetail>(dtoList
                , _IG_UserDetailRepository
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
