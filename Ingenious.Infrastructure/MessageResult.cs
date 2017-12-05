using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure
{
    /// <summary>
    /// 提供返回状态和消息的模板类
    /// </summary>
    public struct MessageResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
