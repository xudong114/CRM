
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Infrastructure;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_EntityRepository : IRepository<G_Entity>
    {
        /// <summary>
        /// 获取最大编码
        /// </summary>
        /// <returns></returns>
        string GetMaxCode();
        PagedResult<G_Entity> GetAll(int pageIndex, int pageSize, ISpecification<G_Entity> spec, string sort = "createddate_desc");
    }
}
