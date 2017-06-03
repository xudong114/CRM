﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    public class RoleFunction : AggregateRoot
    {
        public Guid RoleId { get; set; }
        public Guid FunctionId { get; set; }

    }
}
