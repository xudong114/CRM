using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{

    /// <summary>
    /// 店铺信息
    /// </summary>
    [DisplayName("店铺信息")]
    public class F_StoreDTO : F_ModelRoot
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        [DisplayName("店铺名称")]
        public string Name { get; set; }
        /// <summary>
        /// 店铺编码
        /// </summary>
        [DisplayName("店铺编码")]
        public string Code { get; set; }
        /// <summary>
        /// 所属站点
        /// </summary>
        [DisplayName("所属站点")]
        public Guid WebsiteId { get; set; }
        /// <summary>
        /// 店铺英文名称
        /// </summary>
        [DisplayName("店铺英文名称")]
        public string EnglishName { get; set; }
        /// <summary>
        /// 店铺logo
        /// </summary>
        [DisplayName("logo")]
        public string Logo { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        [DisplayName("开始日期")]
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DisplayName("结束日期")]
        public DateTime EndDate { get; set; }
    }

    public class F_StoreDTOList : List<F_StoreDTO>
    { }
}
