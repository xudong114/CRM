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
        private readonly IUserRepository _IUserRepository;
        public ClientService(IRepositoryContext context,
    IClientRepository iClientRepository,
            IActivityService iActivityService,
            IUserRepository IUserRepository)
            : base(context)
        {
            this._IClientRepository = iClientRepository;
            this._IActivityService = iActivityService;
            this._IUserRepository = IUserRepository;
        }


        public DTO.ClientDTOList GetAll(string keywords = "", Guid? departmentId = null)
        {
            var clients = new ClientDTOList();
            ISpecification<Client> spec = Specification<Client>.Eval(item => true);
            spec = new AndSpecification<Client>(spec, 
                Specification<Client>.Eval(item => 
                    keywords == "" || 
                    item.Name.Contains(keywords)));
            spec = new AndSpecification<Client>(spec,
                Specification<Client>.Eval(item =>
                    !departmentId.HasValue ||
                    item.DepartmentId==departmentId.Value));

            this._IClientRepository.GetAll(spec).ToList().ForEach(item =>
                clients.Add(AutoMapper.Mapper.Map<Client, ClientDTO>(item))
                );
            this.AppendUserInfo(clients, this._IUserRepository.Data);
            return clients;
        }

        public DTO.ClientDTO GetByKey(Guid id)
        {
            var model = this._IClientRepository.GetByKey(id);
            var dto = AutoMapper.Mapper.Map<Client, ClientDTO>(model);
            this.AppendUserInfo(dto, this._IUserRepository.Data);
            return dto;
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
                        entity.Province = dto.Province;
                        entity.City = dto.City;
                        entity.Country = dto.Country;
                        entity.Street = dto.Street;
                        entity.OfficePhone = dto.OfficePhone;
                        entity.Comment = dto.Comment;
                        entity.DepartmentId = dto.DepartmentId;
                        entity.GradeId = dto.GradeId;
                        entity.IndustryId = dto.IndustryId;
                        entity.Postcode = dto.Postcode;
                        entity.Fax = dto.Fax;
                        entity.Website = dto.Website;
                        entity.Weibo = dto.Weibo;
                        entity.Headcount = dto.Headcount;
                        entity.Saleroom = dto.Saleroom;
                        entity.ModifiedBy = dto.ModifiedBy;
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
