using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.DataSource;
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
    public class F_OrderRecordService : ApplicationService, IF_OrderRecordService 
    {
        private readonly IF_OrderRecordRepository _IF_OrderRecordRepository;
        public F_OrderRecordService(IRepositoryContext context,
            IF_OrderRecordRepository iF_OrderRecordRepository)
            : base(context)
        {
            this._IF_OrderRecordRepository = iF_OrderRecordRepository;
        }

        public F_OrderRecordDTOList GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ComplexOrderRecord> GetOrderRecordByOrderId(Guid orderId)
        {
            return this._IF_OrderRecordRepository.GetOrderRecordByOrderId(orderId).ToList();
        }


        public DTO.F_OrderRecordDTO GetByKey(System.Guid id)
        {
            var user = this._IF_OrderRecordRepository.GetByKey(id);

            var userDTO = Mapper.Map<F_OrderRecord ,F_OrderRecordDTO>(user);

            return userDTO;
        }

        public F_OrderRecordDTO Create(F_OrderRecordDTO dto)
        {
            return base.F_Create<F_OrderRecordDTO, F_OrderRecord>(dto
                , _IF_OrderRecordRepository
                , dtoAction => { }); 
        }

        public List<F_OrderRecordDTO> Update(System.Collections.Generic.List<F_OrderRecordDTO> dtoList)
        {
            return base.F_Update<F_OrderRecordDTO, List<F_OrderRecordDTO>, F_OrderRecord>(dtoList
                , _IF_OrderRecordRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<F_OrderRecordDTO> dtoList)
        {
            base.F_Update<F_OrderRecordDTO, List<F_OrderRecordDTO>, F_OrderRecord>(dtoList
                , _IF_OrderRecordRepository
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
