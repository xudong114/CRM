
using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_OrderRecordRepository : IRepository<G_OrderRecord>
    {
        IQueryable<G_OrderRecord> GetAll(ISpecification<G_OrderRecord> spec);

        IQueryable<G_ComplexOrderRecord> GetOrderRecordByOrderId(Guid orderId);
    }
}
