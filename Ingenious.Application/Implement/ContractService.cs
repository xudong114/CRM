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
    public class ContractService : ApplicationService, IContractService
    {
        private readonly IContractRepository _IContractRepository;
        private readonly IDepartmentService _IDepartmentService;
        private readonly IUserService _IUserService;
        public ContractService(IRepositoryContext context,
    IContractRepository iContractRepository,
            IDepartmentService iDepartmentService,
            IUserService iUserService)
            : base(context)
        {
            this._IContractRepository = iContractRepository;
            this._IDepartmentService = iDepartmentService;
            this._IUserService = iUserService;
        }


        public DTO.ContractDTOList GetAll(string keywords = "")
        {
            var Contracts = new ContractDTOList();
            ISpecification<Contract> spec = Specification<Contract>.Eval(item => true);
            spec = new AndSpecification<Contract>(spec, 
                Specification<Contract>.Eval(item => 
                    keywords == "" || 
                    item.Title.Contains(keywords)));

            this._IContractRepository.GetAll(spec).ToList().ForEach(item =>
                Contracts.Add(AutoMapper.Mapper.Map<Contract, ContractDTO>(item))
                );
            foreach (var item in Contracts)
            {
                item.Department =  this._IDepartmentService.GetByKey(item.DepartmentId);
                item.Owner = this._IUserService.GetByKey(item.OwnerId);
                if (item.PrincipalId.HasValue)
                {
                    item.Principal = this._IUserService.GetByKey(item.PrincipalId.Value);
                }
            }
            return Contracts;
        }

        public DTO.ContractDTO GetByKey(Guid id)
        {
            var model = this._IContractRepository.GetByKey(id);
            var item = AutoMapper.Mapper.Map<Contract, ContractDTO>(model);
            item.Department = this._IDepartmentService.GetByKey(item.DepartmentId);
            item.Owner = this._IUserService.GetByKey(item.OwnerId);
            if (item.PrincipalId.HasValue)
            {
                item.Principal = this._IUserService.GetByKey(item.PrincipalId.Value);
            }
            return item;
        }

        public DTO.ContractDTO Create(DTO.ContractDTO dto)
        {

            //var model = base.Create<ContractDTO, Contract>(dto
            //        , _IContractRepository
            //        , dtoAction =>{});

            var model = this._IContractRepository.Create(AutoMapper.Mapper.Map<ContractDTO, Contract>(dto));

            return AutoMapper.Mapper.Map<Contract, ContractDTO>(model);
        }

        public List<DTO.ContractDTO> Update(List<DTO.ContractDTO> dtoList)
        {
            return base.Update<ContractDTO, List<ContractDTO>, Contract>(dtoList
                    , _IContractRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        entity.Title = dto.Title;
                        entity.ClientId = dto.ClientId;
                        entity.TotalAmount = dto.TotalAmount;
                        entity.BeginDate = dto.BeginDate;
                        entity.EndDate = dto.EndDate;
                        entity.OwnerId = dto.OwnerId;
                        entity.DepartmentId = dto.DepartmentId;
                        entity.Status = dto.Status;
                        entity.Payment = dto.Payment;
                        entity.ProductId = dto.ProductId;
                        entity.Content = dto.Content;
                        entity.ClientPrincipal = dto.ClientPrincipal;
                        entity.PrincipalId = dto.PrincipalId;
                        entity.ContractedDate = dto.ContractedDate;
                        entity.Comment = dto.Comment;
                        entity.ModifiedBy = dto.ModifiedBy;
                    });
        }

        public void Delete(List<DTO.ContractDTO> dtoList)
        {
            base.Update<ContractDTO, List<ContractDTO>, Contract>(dtoList
                , _IContractRepository
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
