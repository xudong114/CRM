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
    public class RechargeService : ApplicationService, IRechargeService
    {
        private readonly IRechargeRepository _IRechargeRepository;
        private readonly IUserRepository _IUserRepository;
        public RechargeService(IRepositoryContext context,
    IRechargeRepository iRechargeRepository,
            IUserRepository iUserRepository)
            : base(context)
        {
            this._IRechargeRepository = iRechargeRepository;
            this._IUserRepository = iUserRepository;
        }


        public DTO.RechargeDTOList GetAll()
        {
            var Recharges = new RechargeDTOList();
            ISpecification<Recharge> spec = Specification<Recharge>.Eval(item => true);

            this._IRechargeRepository.GetAll(spec).ToList().ForEach(item =>
                Recharges.Add(AutoMapper.Mapper.Map<Recharge, RechargeDTO>(item))
                );
            this.AppendUserInfo(Recharges,this._IUserRepository.Data);
            return Recharges;
        }

        public DTO.RechargeDTO GetByKey(Guid id)
        {
            var model = this._IRechargeRepository.GetByKey(id);
            var dto = AutoMapper.Mapper.Map<Recharge, RechargeDTO>(model);
            this.AppendUserInfo(dto, this._IUserRepository.Data);
            return dto;
        }

        public DTO.RechargeDTOList GetByAccountId(Guid id)
        {
            var Recharges = new RechargeDTOList();
            ISpecification<Recharge> spec = Specification<Recharge>.Eval(item => true);
            this._IRechargeRepository.GetByAccountId(id).ToList().ForEach(item =>
              Recharges.Add(AutoMapper.Mapper.Map<Recharge, RechargeDTO>(item))
              );
            this.AppendUserInfo(Recharges, this._IUserRepository.Data);
            return Recharges;
        }

        public DTO.RechargeDTO Create(DTO.RechargeDTO dto)
        {

            var model = base.Create<RechargeDTO, Recharge>(dto
                    , _IRechargeRepository
                    , dtoAction => {});

            return model;
        }

        public List<DTO.RechargeDTO> Update(List<DTO.RechargeDTO> dtoList)
        {
            return base.Update<RechargeDTO, List<RechargeDTO>, Recharge>(dtoList
                    , _IRechargeRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        entity.ModifiedBy = dto.ModifiedBy;
                    });
        }

        public void Delete(List<DTO.RechargeDTO> dtoList)
        {
            base.Update<RechargeDTO, List<RechargeDTO>, Recharge>(dtoList
                , _IRechargeRepository
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
