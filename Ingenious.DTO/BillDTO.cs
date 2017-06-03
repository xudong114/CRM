using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 经销商账单
    /// </summary>
    public class BillDTO : ModelRoot
    {
        /// <summary>
        /// 关联账号标识
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// 关联账号
        /// </summary>
        public AccountDTO Account { get; set; }
        /// <summary>
        /// 关联合同标识
        /// </summary>
        public Guid ContractId { get; set; }
        /// <summary>
        /// 关联合同
        /// </summary>
        public ContractDTO Contract { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal Money { get; set; }

    }

    public class BillDTOList:List<BillDTO>
    { }
}
