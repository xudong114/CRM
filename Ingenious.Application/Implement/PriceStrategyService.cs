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
    public class PriceStrategyService : ApplicationService, IPriceStrategyService
    {

        private readonly IPriceStrategyRepository _IPriceStrategyRepository;
        private readonly IUserRepository _IUserRepository;
        public PriceStrategyService(IRepositoryContext context,
            IPriceStrategyRepository iPriceStrategyRepository,
            IUserRepository iUserRepository)
            : base(context)
        {
            this._IUserRepository = iUserRepository;
            this._IPriceStrategyRepository = iPriceStrategyRepository;
        }

        public PriceStrategyDTO GetPriceStrategyByUserId(Guid id)
        {
            var user = this._IUserRepository.GetByKey(id);

            ISpecification<PriceStrategy> spec = Specification<PriceStrategy>.Eval(model => true);
            spec = new AndSpecification<PriceStrategy>(spec,
                Specification<PriceStrategy>.Eval(

                model => (user.Department == null && model.IsDefault)
                    || (model.Id.Equals(user.DepartmentId))

                ));

            var priceStrategy = this._IPriceStrategyRepository.GetAll(spec).FirstOrDefault();
            return AutoMapper.Mapper.Map<PriceStrategy, PriceStrategyDTO>(priceStrategy);
        }

        public DTO.PriceStrategyDTOList GetAll(string keywords = "")
        {
            var PriceStrategys = new PriceStrategyDTOList();
            ISpecification<PriceStrategy> spec = Specification<PriceStrategy>.Eval(item => true);
            spec = new AndSpecification<PriceStrategy>(spec,
                Specification<PriceStrategy>.Eval(item =>
                    keywords == "" ||
                    item.Name.Contains(keywords)));

            this._IPriceStrategyRepository.GetAll(spec).ToList().ForEach(item =>
                PriceStrategys.Add(AutoMapper.Mapper.Map<PriceStrategy, PriceStrategyDTO>(item))
                );
            return PriceStrategys;
        }

        public PriceStrategyDTO GetByKey(Guid id)
        {
            var model = this._IPriceStrategyRepository.GetByKey(id);

            return AutoMapper.Mapper.Map<PriceStrategy, PriceStrategyDTO>(model);
        }

        public PriceStrategyDTO Create(PriceStrategyDTO dto)
        {
            return base.Create<PriceStrategyDTO, PriceStrategy>(dto
                    , _IPriceStrategyRepository
                    , dtoAction =>{});
        }

        public List<PriceStrategyDTO> Update(List<PriceStrategyDTO> dtoList)
        {
            return base.Update<PriceStrategyDTO, List<PriceStrategyDTO>, PriceStrategy>(dtoList
                    , _IPriceStrategyRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        entity.IsAgent = dto.IsAgent;
                        entity.IsDefault = dto.IsDefault;
                        entity.Comment = dto.Comment;
                        entity.Name = dto.Name;
                        entity.ModifiedBy = dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                    });
        }

        public void Delete(List<PriceStrategyDTO> dtoList)
        {
            base.Update<PriceStrategyDTO, List<PriceStrategyDTO>, PriceStrategy>(dtoList
                , _IPriceStrategyRepository
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
