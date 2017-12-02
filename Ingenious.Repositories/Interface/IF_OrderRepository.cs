
using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_OrderRepository : IRepository<F_Order>
    {
        PagedResult<ComplexOrder> GetAll(int pageIndex, int pageSize, ISpecification<F_Order> spec, string sort = "");
        IQueryable<Infrastructure.KeyValue<string, int>> AssignOrderClerk(Guid websiteId);
    }
}
