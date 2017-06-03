using AutoMapper;
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

namespace Ingenious.Application.Implement
{
    public class DepartmentService : ApplicationService, IDepartmentService 
    {
        private readonly IDepartmentRepository _IDepartmentRepository;
        public DepartmentService(IRepositoryContext context,
            IDepartmentRepository iDepartmentRepository): base(context)
        {
            this._IDepartmentRepository = iDepartmentRepository;
        }

        public DepartmentDTOList GetAll(bool? isBranch = null, string keywords = "")
        {
            var list = new DepartmentDTOList();
            ISpecification<Department> spec = Specification<Department>.Eval(d => true);
            spec = new AndSpecification<Department>(spec, Specification<Department>.Eval(item=>item.Name.Contains(keywords)));
            if(isBranch.HasValue)
            {
                spec = new AndSpecification<Department>(spec, Specification<Department>.Eval(item => item.IsBranch == isBranch.Value));
            }

            this._IDepartmentRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<Department, DepartmentDTO>(item)));

            return list;
        }

        public DTO.DepartmentDTO GetByKey(System.Guid id)
        {
            var department = this._IDepartmentRepository.GetByKey(id);

            var departmentDTO = Mapper.Map<Department, DepartmentDTO>(department);


            return departmentDTO;
        }

        public DTO.DepartmentDTO Create(DTO.DepartmentDTO dto)
        {
            return base.Create<DepartmentDTO, Department>(dto
                , _IDepartmentRepository
                , dtoAction =>{});
        }

        public System.Collections.Generic.List<DTO.DepartmentDTO> Update(System.Collections.Generic.List<DTO.DepartmentDTO> dtoList)
        {
            return base.Update<DepartmentDTO, List<DepartmentDTO>, Department>(dtoList
                , _IDepartmentRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.PriceStrategyId = dto.PriceStrategyId;
                    entity.Name = dto.Name;
                    entity.ParentId = dto.ParentId;
                    entity.Principal = dto.Principal;
                    entity.Phone = dto.Phone;
                    entity.OfficePhone = dto.OfficePhone;
                    entity.Address = dto.Address;
                    entity.Website = dto.Website;
                    entity.ModifiedBy = dto.ModifiedBy;
                    entity.ModifiedDate = DateTime.Now;
                    entity.Version = dto.Version;
                });
        }

        public void Delete(System.Collections.Generic.List<DTO.DepartmentDTO> dtoList)
        {
            base.Update<DepartmentDTO, List<DepartmentDTO>, Department>(dtoList
            , _IDepartmentRepository
            , dto => dto.Id
            , (dto, entity) =>
            {
                entity.IsActive = dto.IsActive;
                entity.ModifiedDate = dto.ModifiedDate;
                entity.ModifiedBy = dto.ModifiedBy;
            });
        }

        public string GetChildren(Guid? partntId,List<DepartmentDTO> departmentList)
        {
            var department = new DepartmentDTO();
            //var partnt = departmentList.Find(item => !item.ParentId.HasValue);
            var htmlString = new StringBuilder();
            var partntItem = departmentList.Find(model => model.Id == partntId);
            if (partntItem!=null)
            {
                htmlString.Append("<li>");
                htmlString.Append(string.Format("<a id='{1}' >{0}</a>", partntItem.Name, partntItem.Id));
                htmlString.Append("</li>");
            }
            this.GetItems(partntId, departmentList, htmlString);

            return htmlString.ToString();
        }

        private void GetItems(Guid? partntId, List<DepartmentDTO> departmentList, StringBuilder htmlString)
        {
            if (departmentList.Where(model => model.ParentId == partntId).Count() > 0)
            {
                htmlString.Append("<ul>");
                foreach (var item in departmentList.Where(model => model.ParentId == partntId))
                {
                    htmlString.Append("<li>");
                    htmlString.Append(string.Format("<a id='{1}' >{0}</a>", item.Name,item.Id));
                    htmlString.Append("</li>");
                    if (departmentList.Where(model => model.ParentId == item.Id).Count() > 0)
                    {
                        this.GetItems(item.Id, departmentList, htmlString);
                    }
                }
                htmlString.Append("</ul>");
            }
        }
    
    }
}
