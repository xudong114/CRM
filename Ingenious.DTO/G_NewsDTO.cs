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
    /// 新闻
    /// </summary>
    [DisplayName("新闻")]
    public class G_NewsDTO : F_ModelRoot
    {
        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("标题")]
        [Required(ErrorMessage="标题是必填项")]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public string Content { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        [DisplayName("分类")]
        [Required(ErrorMessage = "分类是必填项")]
        public string Code { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        [DisplayName("链接")]
        public string Link { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        [DisplayName("来源")]
        public string Source { get; set; }
        /// <summary>
        /// 形象图
        /// </summary>
        [DisplayName("形象图")]
        public string Logo { get; set; }
        /// <summary>
        /// 唯一数字编码
        /// </summary>
        [DisplayName("唯一数字编码")]
        public int Key { get; set; }

        public string TimeLabel
        {
            get
            {
                return this.CreatedDate.ToString("yyyy-MM-dd");
            }
        }
    }
}
