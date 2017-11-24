
using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_OrderRepository : IRepository<G_Order>
    {
        /// <summary>
        /// 查询订单数据
        /// </summary>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="spec">过滤条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns></returns>
        PagedResult<G_ComplexOrder> GetAll(int pageNumber, int pageSize, ISpecification<G_Order> spec, string sort = "");
        /// <summary>
        /// 获取订单所有状态的数量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        ComplexStatusCount GetComplexStatusCount(ISpecification<G_Order> spec);
        /// <summary>
        /// 获取订单状态的数量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        int GetStatusCount(ISpecification<G_Order> spec);
        IQueryable<Infrastructure.KeyValue<string, int>> AssignOrderClerk(Guid websiteId);
    }
}
