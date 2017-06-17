using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
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
    public class ClientContactService : ApplicationService, IClientContactService
    {
        private readonly IUserService _IUserService;
        private readonly IClientContactRepository _IClientContactRepository;

        public ClientContactService(IRepositoryContext context,
            IClientContactRepository iClientContactRepository,
            IUserService iUserService)
            : base(context)
        {
            this._IClientContactRepository = iClientContactRepository;
            this._IUserService = iUserService;
        }

        public DTO.ClientContactDTOList GetContactsByClientId(Guid clientId)
        {
            var list = new ClientContactDTOList();
            this._IClientContactRepository.GetContactsByClientId(clientId)
                .ToList()
                .ForEach(item =>
                {
                    list.Add(AutoMapper.Mapper.Map<ClientContact, ClientContactDTO>(item));
                });
            return list;
        }

         public DTO.ClientContactDTO GetByKey(Guid id)
         {
             return AutoMapper.Mapper.Map<ClientContact, ClientContactDTO>(this._IClientContactRepository.GetByKey(id));
         }

         public DTO.ClientContactDTO Create(DTO.ClientContactDTO dto)
         {
             var model = base.Create<ClientContactDTO, ClientContact>(dto
                     , _IClientContactRepository
                     , dtoAction => { });

             return model;
         }

         public List<DTO.ClientContactDTO> Update(List<DTO.ClientContactDTO> dtoList)
         {
             return base.Update<ClientContactDTO, List<ClientContactDTO>, ClientContact>(dtoList
                , _IClientContactRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.Comment = dto.Comment;
                    entity.Email = dto.Email;
                    entity.Name = dto.Name;
                    entity.OfficePhone = dto.OfficePhone;
                    entity.Phone = dto.Phone;
                    entity.Title = dto.Title;
                    entity.Wechat = dto.Wechat;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
         }

         public void Delete(List<DTO.ClientContactDTO> dtoList)
         {
             base.Update<ClientContactDTO, List<ClientContactDTO>, ClientContact>(dtoList
               , _IClientContactRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
         }
     }
}
