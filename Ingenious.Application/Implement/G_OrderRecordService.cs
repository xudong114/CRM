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
    public class G_OrderRecordService : ApplicationService, IG_OrderRecordService
    {
        private readonly IG_OrderRecordRepository _IG_OrderRecordRepository;
        public G_OrderRecordService(IRepositoryContext context,
            IG_OrderRecordRepository iG_OrderRecordRepository)
            : base(context)
        {
            this._IG_OrderRecordRepository = iG_OrderRecordRepository;
        }

        public G_OrderRecordDTOList GetAll()
        {
            throw new NotImplementedException();
        }

        public List<G_ComplexOrderRecord> GetOrderRecordByOrderId(Guid orderId)
        {
            return this._IG_OrderRecordRepository.GetOrderRecordByOrderId(orderId).ToList();
        }


        public DTO.G_OrderRecordDTO GetByKey(System.Guid id)
        {
            var user = this._IG_OrderRecordRepository.GetByKey(id);

            var userDTO = Mapper.Map<G_OrderRecord, G_OrderRecordDTO>(user);

            return userDTO;
        }

        public G_OrderRecordDTO Create(G_OrderRecordDTO dto)
        {
            return base.F_Create<G_OrderRecordDTO, G_OrderRecord>(dto
                , _IG_OrderRecordRepository
                , dtoAction => { });
        }

        public List<G_OrderRecordDTO> Update(System.Collections.Generic.List<G_OrderRecordDTO> dtoList)
        {
            return base.F_Update<G_OrderRecordDTO, List<G_OrderRecordDTO>, G_OrderRecord>(dtoList
                , _IG_OrderRecordRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<G_OrderRecordDTO> dtoList)
        {
            base.F_Update<G_OrderRecordDTO, List<G_OrderRecordDTO>, G_OrderRecord>(dtoList
                , _IG_OrderRecordRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }




        List<G_LoanProductDTO> IG_OrderRecordService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
