
using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_OrderRecordRepository : IRepository<F_OrderRecord>
    {
        IQueryable<F_OrderRecord> GetAll(ISpecification<F_OrderRecord> spec);

        IQueryable<ComplexOrderRecord> GetOrderRecordByOrderId(Guid orderId);
    }
}
