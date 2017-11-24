using Ingenious.Infrastructure;
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
    /// 佳居贷组织机构（金融代理商）
    /// </summary>
    [DisplayName("佳居贷组织机构（金融代理商）")]
    public class G_EntityDTO : F_ModelRoot
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        [DisplayName("系统管理员")]
        public Guid? UserId { get; set; }
        /// <summary>
        /// 系统管理员
        /// </summary>
        public G_UserDTO Admin { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        [DisplayName("机构编码")]
        public string Code { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [DisplayName("负责人")]
        [Required(ErrorMessage = "负责人是必填项")]
        public string Name { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        [DisplayName("机构名称")]
        [Required(ErrorMessage = "机构名称是必填项")]
        public string EntityName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        [Required(ErrorMessage = "机构名称是必填项")]
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        [DisplayName("办公电话")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 所在省份
        /// </summary>
        [DisplayName("所在省份")]
        public string Province { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        [DisplayName("所在城市")]
        public string City { get; set; }
        /// <summary>
        /// 所在县区
        /// </summary>
        [DisplayName("所在县区")]
        public string Country { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        [DisplayName("街道")]
        public string Street { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        [DisplayName("行业")]
        public string Industry { get; set; }
        /// <summary>
        /// 法人身份证号码
        /// </summary>
        [DisplayName("法人身份证号码")]
        public string IDNo { get; set; }
        /// <summary>
        /// 法人身份证照片（正反面）
        /// </summary>
        [DisplayName("法人身份证照片（正反面）")]
        public string IDImg { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        [DisplayName("营业执照")]
        public string BusinessLicenseImg { get; set; }
        /// <summary>
        /// 营业执照编号
        /// </summary>
        [DisplayName("营业执照编号")]
        public string BusinessLicenseNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
    }


    public class G_EntityDTOList : List<G_EntityDTO>
    { }

    public class G_EntityDTOListWithPagination:PagedResult<G_EntityDTO>
    {

    }
}
