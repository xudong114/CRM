
using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_OrderRepository : IRepository<F_Order>
    {
        IQueryable<ComplexOrder> GetAll(ISpecification<F_Order> spec);
        IQueryable<Infrastructure.KeyValue<string, int>> AssignOrderClerk(Guid websiteId);
    }
}
