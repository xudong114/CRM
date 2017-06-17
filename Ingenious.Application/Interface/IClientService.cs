﻿using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IClientService : IApplication<ClientDTO>
    {
        ClientDTOList GetAll(string keywords = "", Guid? departmentId = null);
    }
}
