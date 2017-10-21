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
    public class F_BankOptionRepository : EntityFrameworkRepository<F_BankOption>, IF_BankOptionRepository
    {
        public F_BankOptionRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<F_BankOption> GetAll(ISpecification<F_BankOption> spec, string sort = "order_desc")
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            var query = context.F_BankOptions.Where(spec.GetExpression());

            switch (sort)
            {
                case "order_desc":
                    {
                        query = query.OrderByDescending(item => item.F_Bank.Order);
                    } break;
                case "order":
                    {
                        query = query.OrderBy(item => item.F_Bank.Order);
                    } break;
                case "createddate_desc":
                    {
                        query = query.OrderByDescending(item => item.F_Bank.CreatedDate);
                    } break;
                case "createddate":
                    {
                        query = query.OrderBy(item => item.F_Bank.CreatedDate);
                    } break;
            }

            return query;
        }

        public F_BankOption GetBankOptionByBankId(Guid bankId)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.F_BankOptions.Where(item => item.F_BankId.Equals(bankId));
            return query.ToList().FirstOrDefault();
        }

    }
}
