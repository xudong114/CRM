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
    public class F_BankRepository : EntityFrameworkRepository<F_Bank>, IF_BankRepository
    {
        public F_BankRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_Bank> GetAll(ISpecification<F_Bank> spec, string sort = "order")
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.F_Banks.Where(spec.GetExpression());
            switch (sort)
            {
                case "order_desc":
                    {
                        query = query.OrderByDescending(item => item.Order);
                    }
                    break;
                case "order":
                    {
                        query = query.OrderBy(item => item.Order);
                    }
                    break;
                case "createddate_desc":
                    {
                        query = query.OrderByDescending(item => item.CreatedDate);
                    }
                    break;
                case "createddate":
                    {
                        query = query.OrderBy(item => item.CreatedDate);
                    }
                    break;
            }
            return query;
        }

        public F_Bank GetBankByUserId(Guid id)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from b in context.F_Banks
                        join u in context.F_UserDetails on b.Code equals u.BankCode
                        select b;

            return query.ToList().FirstOrDefault();
        }

        public IQueryable<Infrastructure.KeyValue<string, int>> AssignOrderToClerk(string bankCode)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            var query = from ud in context.F_UserDetails
                        join u in context.F_Users on ud.F_UserId equals u.Id
                        join o in context.F_Orders on ud.Code equals o.BankClerk into oData
                        from od in oData.DefaultIfEmpty()
                        where ud.BankCode == bankCode
                        && (u.UserType == Infrastructure.Enum.F_UserTypeEnum.BC)
                        && (u.IsActive)
                        select new Infrastructure.KeyValue<string, int> { Key = ud.Code, Value = oData.Count() };
            return query;
        }

    }
}
