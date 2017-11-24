using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 贷款产品
    /// </summary>
    public class G_LoanProductDTO : F_ModelRoot
    {
        /// <summary>
        /// Logo
        /// </summary>
        [DisplayName("Logo")]
        public string Logo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DisplayName("产品名称")]
        [Required(ErrorMessage = "产品名称为必填项")]
        public string Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [DisplayName("编码")]
        [Required(ErrorMessage = "产品编码为必填项")]
        public string Code { get; set; }

        /// <summary>
        /// 利率
        /// </summary>
        [DisplayName("利率")]

        public string Rate { get; set; }
        /// <summary>
        /// 可贷金额
        /// </summary>
        [DisplayName("可贷金额")]
        public string Loan { get; set; }
        /// <summary>
        /// 还款方式
        /// </summary>
        [DisplayName("还款方式")]
        public string Terms { get; set; }
        /// <summary>
        /// 贷款优势
        /// </summary>
        [DisplayName("贷款优势")]
        public string Tip { get; set; }

        /// <summary>
        /// 申请条件
        /// </summary>
        [DisplayName("申请条件")]
        public string Policy { get; set; }
    }

    public class G_LoanProductDTOList : List<G_LoanProductDTO>
    { }

}
