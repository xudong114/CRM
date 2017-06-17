﻿using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Repositories;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Implement
{
    public class BillService : ApplicationService, IBillService
    {
        private readonly IBillRepository _IBillRepository;
        private readonly IUserRepository _IUserRepository;
        public BillService(IRepositoryContext context,
    IBillRepository iBillRepository,
            IUserRepository iUserRepository)
            : base(context)
        {
            this._IBillRepository = iBillRepository;
            this._IUserRepository = iUserRepository;
        }


        public DTO.BillDTOList GetAll()
        {
            var Bills = new BillDTOList();
            ISpecification<Bill> spec = Specification<Bill>.Eval(item => true);

            this._IBillRepository.GetAll(spec).ToList().ForEach(item =>
                Bills.Add(AutoMapper.Mapper.Map<Bill, BillDTO>(item))
                );
            this.AppendUserInfo(Bills, this._IUserRepository.Data);
            return Bills;
        }

        public DTO.BillDTO GetByKey(Guid id)
        {
            var model = this._IBillRepository.GetByKey(id);
            var dto = AutoMapper.Mapper.Map<Bill, BillDTO>(model);
            this.AppendUserInfo(dto, this._IUserRepository.Data);
            return dto;
        }

        public DTO.BillDTOList GetByAccountId(Guid id)
        {
            var Bills = new BillDTOList();
            ISpecification<Bill> spec = Specification<Bill>.Eval(item => true);
            this._IBillRepository.GetByAccountId(id).ToList().ForEach(item =>
              Bills.Add(AutoMapper.Mapper.Map<Bill, BillDTO>(item))
              );
            this.AppendUserInfo(Bills, this._IUserRepository.Data);
            return Bills;
        }

        public DTO.BillDTO Create(DTO.BillDTO dto)
        {

            var model = base.Create<BillDTO, Bill>(dto
                    , _IBillRepository
                    , dtoAction => {});

            return model;
        }

        public List<DTO.BillDTO> Update(List<DTO.BillDTO> dtoList)
        {
            return base.Update<BillDTO, List<BillDTO>, Bill>(dtoList
                    , _IBillRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        entity.ModifiedBy = dto.ModifiedBy;
                    });
        }

        public void Delete(List<DTO.BillDTO> dtoList)
        {
            base.Update<BillDTO, List<BillDTO>, Bill>(dtoList
                , _IBillRepository
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
