using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class F_OrderRecordRepository : EntityFrameworkRepository<F_OrderRecord>, IF_OrderRecordRepository
    {
        public F_OrderRecordRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_OrderRecord> GetAll(ISpecification<F_OrderRecord> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_OrderRecords.Where(spec.GetExpression());
        }

        public IQueryable<ComplexOrderRecord> GetOrderRecordByOrderId(Guid orderId)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from or in context.G_OrderRecords
                        join u in context.G_UserDetails on or.UserId equals u.G_UserId
                        where or.OrderId == orderId
                        select new ComplexOrderRecord
                        {
                            Id = or.Id,
                            CreatedDate = or.CreatedDate,
                            IsActive = or.IsActive,
                            ModifiedDate = or.ModifiedDate,
                            OfficePhone = u.OfficePhone,
                            OrderId = or.OrderId,
                            Remark = or.Remark,
                            Status = or.Status,
                            UserId = or.UserId,
                            UserCode = u.Code,
                            UserName = u.Name
                        };
            return query;
        }

    }
}
