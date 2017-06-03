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
    public class ClientService : ApplicationService, IClientService
    {
        private readonly IClientRepository _IClientRepository;
        private readonly IActivityService _IActivityService;
        public ClientService(IRepositoryContext context,
    IClientRepository iClientRepository,
            IActivityService iActivityService)
            : base(context)
        {
            this._IClientRepository = iClientRepository;
            this._IActivityService = iActivityService;
        }


        public DTO.ClientDTOList GetAll(string keywords = "")
        {
            var clients = new ClientDTOList();
            ISpecification<Client> spec = Specification<Client>.Eval(item => true);
            spec = new AndSpecification<Client>(spec, 
                Specification<Client>.Eval(item => 
                    keywords == "" || 
                    item.Name.Contains(keywords)));

            this._IClientRepository.GetAll(spec).ToList().ForEach(item =>
                clients.Add(AutoMapper.Mapper.Map<Client, ClientDTO>(item))
                );
            return clients;
        }

        public DTO.ClientDTO GetByKey(Guid id)
        {
            var model = this._IClientRepository.GetByKey(id);

            return AutoMapper.Mapper.Map<Client, ClientDTO>(model);
        }

        public DTO.ClientDTO Create(DTO.ClientDTO dto)
        {

            var model = base.Create<ClientDTO, Client>(dto
                    , _IClientRepository
                    , dtoAction => {});

            //var activityDTO = new ActivityDTO
            //{
            //    Content = string.Format("创建了客户：{0}", model.Name),
            //    ReferenceId = model.Id,
            //    CreatedBy = model.CreatedBy,
            //    ModifiedBy = model.ModifiedBy,
            //    UserId = model.CreatedBy
            //};

            //this._IActivityService.Create(activityDTO);

            return model;
        }

        public List<DTO.ClientDTO> Update(List<DTO.ClientDTO> dtoList)
        {
            return base.Update<ClientDTO, List<ClientDTO>, Client>(dtoList
                    , _IClientRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        entity.Name = dto.Name;
                        entity.ModifiedBy = dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;

                    });
        }

        public void Delete(List<DTO.ClientDTO> dtoList)
        {
            base.Update<ClientDTO, List<ClientDTO>, Client>(dtoList
                , _IClientRepository
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
