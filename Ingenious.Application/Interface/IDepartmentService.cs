using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using System;
using System.Collections.Generic;

namespace Ingenious.Application.Interface
{
    public interface IDepartmentService : IApplication<DepartmentDTO>
    {
        DepartmentDTOList GetAll(bool? isBranch = null, string keywords = "");

        string GetChildren(Guid? partntId, List<DepartmentDTO> departmentList);
    }
}
