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
    public class G_OrderRecordRepository : EntityFrameworkRepository<G_OrderRecord>, IG_OrderRecordRepository
    {
        public G_OrderRecordRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<G_OrderRecord> GetAll(ISpecification<G_OrderRecord> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.G_OrderRecords.Where(spec.GetExpression());
        }

        public IQueryable<G_ComplexOrderRecord> GetOrderRecordByOrderId(Guid orderId)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from or in context.G_OrderRecords
                        join u in context.G_UserDetails on or.UserId equals u.G_UserId into udata
                        from ludata in udata.DefaultIfEmpty()
                        join o in context.G_Orders on or.OrderId equals o.Id into odata
                        from lodatain in odata.DefaultIfEmpty()
                        join b in context.G_Banks on lodatain.BankCode equals b.Code into bdata
                        from lbdata in bdata.DefaultIfEmpty()
                        where or.OrderId == orderId
                        select new G_ComplexOrderRecord
                        {
                            Id = or.Id,
                            CreatedDate = or.CreatedDate,
                            IsActive = or.IsActive,
                            ModifiedDate = or.ModifiedDate,
                            OfficePhone = ludata.OfficePhone == "" ? ludata.OfficePhone : ludata.PersonalPhone,
                            OrderId = or.OrderId,
                            Remark = or.Remark,
                            Status = or.Status,
                            UserId = or.UserId,
                            UserCode = ludata.Code,
                            UserName = ludata.Name,
                            BankName = lbdata.Name
                        };
            return query;
        }

    }
}
