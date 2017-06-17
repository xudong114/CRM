using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 活动记录
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ActivityDTO : ModelRoot
    {
        /// <summary>
        /// 关联记录目标标识
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 记录内容
        /// </summary>
        [Required(ErrorMessage = "记录内容不能为空")]
        public string Content { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public Guid ActivityCategoryId { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public DictionaryDTO ActivityCategory { get; set; }
        /// <summary>
        /// 记录类型名称
        /// </summary>
        public string ActivityCategoryName
        {
            get
            {
                return this.ActivityCategory == null
                    ? ""
                    : this.ActivityCategory.Name;
            }
        }

        /// <summary>
        /// 留言时间标签
        /// </summary>
        public string TimeLabel
        {
            get
            {
                Ingenious.Infrastructure.TimeLabel label = new Infrastructure.TimeLabel(this.CreatedDate);

                return label.Label;
            }
        }
        /// <summary>
        /// 评论主体标识
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 评论主体
        /// </summary>
        public ActivityDTO Parent { get; set; }
        /// <summary>
        /// 评论人标识
        /// </summary>
        public Guid UserId { get; set; }
        public UserDTO User { get; set; }
        /// <summary>
        /// 评论人姓名
        /// </summary>
        public string UserName
        {
            get
            {
                if (this.User == null)
                    return string.Empty;
                if (this.User.UserDetail == null)
                    return this.User.UserName;
                return this.User.UserDetail.Name;
            }
        }

    }

    public class ActivityDTOList : List<ActivityDTO>
    { }

}
