using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 权限管理：Area
    /// </summary>
    public class Area : AggregateRoot
    {
        public string Name { get; set; }

        public virtual ICollection<Controller> Controllers { get; set; }
    }

    /// <summary>
    /// 权限管理：Controller
    /// </summary>
    public class Controller : AggregateRoot
    {
        public string Name { get; set; }

        public virtual ICollection<Action> Actions { get; set; }
    }

    /// <summary>
    /// 权限管理：Action
    /// </summary>
    public class Action:AggregateRoot
    {
        public Guid ControllerId { get; set; }

        public string Name{get;set;}

    }
}
