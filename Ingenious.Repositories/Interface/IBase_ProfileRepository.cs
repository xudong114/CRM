using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Infrastructure;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IBase_ProfileRepository : IRepository<Base_Profile>
    {
        /// <summary>
        /// 根据身份证号码获取个人信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        Base_Profile GetProfileByIDNo(string idNo);
        /// <summary>
        /// 获取个人分页数据
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="spec">过滤条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        PagedResult<Base_Profile> GetAll(int pageIndex, int pageSize, ISpecification<Base_Profile> spec, string sort = "createddate_desc");
    }
}
