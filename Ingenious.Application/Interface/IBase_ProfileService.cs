using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IBase_ProfileService : IApplication<Base_ProfileDTO>
    {
        /// <summary>
        /// 根据身份证号码获取个人信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        Base_ProfileDTO GetProfileByIDNo(string idNo);

        /// <summary>
        /// 获取个人分页数据
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="idno">身份证号码</param>
        /// <param name="personalPhone">手机号码</param>
        /// <param name="name">姓名</param>
        /// <param name="nativeplace">籍贯</param>
        /// <param name="wechat">微信号</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="officePhone">办公电话</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        Base_ProfileDTOListWithPagination GetAll(int pageIndex, int pageSize, string idno = "",string personalPhone="", string name = "", string nativeplace = "",string wechat = "",string email = "",string officePhone="", string sort = "createddate_desc");

    }
}
