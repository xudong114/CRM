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
    /// 广告
    /// </summary>
    [DisplayName("广告")]
    public class F_ADDTO : F_ModelRoot
    {
        /// <summary>
        /// 广告名称
        /// </summary>
        [DisplayName("广告名称")]
        [Required(ErrorMessage = "广告名称是必填项")]
        public string Name { get; set; }
        /// <summary>
        /// 广告链接
        /// </summary>
        [DisplayName("广告链接")]
        public string Url { get; set; }
        /// <summary>
        /// 广告图片
        /// </summary>
        [DisplayName("广告图片")]
        [Required(ErrorMessage = "广告图片是必填项")]
        public string Image { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 广告分类
        /// </summary>
        [DisplayName("广告分类")]
        [Required(ErrorMessage = "广告分类是必填项")]
        public string Code { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Remark { get; set; }
    }
}
