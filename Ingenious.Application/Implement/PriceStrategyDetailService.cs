using Ingenious.Application.Interface;
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
    public class PriceStrategyDetailService : ApplicationService, IPriceStrategyDetailService
    {

        private readonly IPriceStrategyDetailRepository _IPriceStrategyDetailRepository;

        public PriceStrategyDetailService(IRepositoryContext context,
            IPriceStrategyDetailRepository iPriceStrategyDetailRepository)
            : base(context)
        {
            this._IPriceStrategyDetailRepository = iPriceStrategyDetailRepository;
        }

        public DTO.PriceStrategyDetailDTOList GetAll(string keywords = "")
        {
            var PriceStrategyDetails = new PriceStrategyDetailDTOList();
            ISpecification<PriceStrategyDetail> spec = Specification<PriceStrategyDetail>.Eval(item => true);

            this._IPriceStrategyDetailRepository.GetAll(spec).ToList().ForEach(item =>
                PriceStrategyDetails.Add(AutoMapper.Mapper.Map<PriceStrategyDetail, PriceStrategyDetailDTO>(item))
                );
            return PriceStrategyDetails;
        }

        public PriceStrategyDetailDTO GetByKey(Guid id)
        {
            var model = this._IPriceStrategyDetailRepository.GetByKey(id);

            return AutoMapper.Mapper.Map<PriceStrategyDetail, PriceStrategyDetailDTO>(model);
        }

        public PriceStrategyDetailDTO Create(PriceStrategyDetailDTO dto)
        {
            return base.Create<PriceStrategyDetailDTO, PriceStrategyDetail>(dto
                    , _IPriceStrategyDetailRepository
                    , dtoAction =>{});
        }

        public List<PriceStrategyDetailDTO> Create(List<PriceStrategyDetailDTO> dtolist)
        {
            var list = new List<PriceStrategyDetail>();
            foreach (var item in dtolist)
            {
                list.Add(AutoMapper.Mapper.Map<PriceStrategyDetailDTO, PriceStrategyDetail>(item));
            }

            this._IPriceStrategyDetailRepository.Create(list);
            return dtolist;
        }

        public List<PriceStrategyDetailDTO> Update(List<PriceStrategyDetailDTO> dtoList)
        {
            return base.Update<PriceStrategyDetailDTO, List<PriceStrategyDetailDTO>, PriceStrategyDetail>(dtoList
                    , _IPriceStrategyDetailRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        entity.RenewPrice = dto.RenewPrice;
                        entity.Price = dto.Price;
                        entity.Minimum = dto.Minimum;
                        entity.DiscountRate = dto.DiscountRate;
                        entity.ModifiedBy = dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                    });
        }

        public void Delete(List<PriceStrategyDetailDTO> dtoList)
        {
            base.Update<PriceStrategyDetailDTO, List<PriceStrategyDetailDTO>, PriceStrategyDetail>(dtoList
                , _IPriceStrategyDetailRepository
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
