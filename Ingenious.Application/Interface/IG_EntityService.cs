using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_EntityService : IApplication<G_EntityDTO>
    {
        /// <summary>
        /// 获取佳居贷经销商分页数据
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页显示记录条数</param>
        /// <param name="keyword">关键字： 机构负责人姓名、机构编码、联系店铺（手机、办公电话）</param>
        /// <param name="province">所在省份</param>
        /// <param name="city">所在城市</param>
        /// <param name="country">所在县区</param>
        /// <param name="isActive">是否可用</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        G_EntityDTOListWithPagination GetAll(int pageIndex, int pageSize, string keyword = "", string province = "", string city = "", string country = "", bool? isActive = true, string sort = "createddate_desc");
    }
}
