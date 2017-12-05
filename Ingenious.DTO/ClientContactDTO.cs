using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class ClientContactDTO : ModelRoot
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [DisplayName("公司名称")]
        public Guid ClientId { get; set; }
        [DisplayName("公司名称")]
        public ClientDTO Client { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [DisplayName("职务")]
        public string Title { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        public string Phone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [DisplayName("电子邮件")]
        public string Email { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        [DisplayName("微信")]
        public string Wechat { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Comment { get; set; }
    }

    public class ClientContactDTOList:List<ClientContactDTO>
    {

    }
}
