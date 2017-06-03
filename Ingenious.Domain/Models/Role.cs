using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    public class Role : AggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
