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
    public class Base_BankRepository : EntityFrameworkRepository<Base_Bank>, IBase_BankRepository
    {
        public Base_BankRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<Base_Bank> GetAll(int pageIndex, int pageSize, ISpecification<Base_Bank> spec, string sort = "order")
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.Base_Banks.Where(spec.GetExpression());
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

        public int GetMaxCode()
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            return context.G_Banks.Max(item => item.Order);
        }
    }
}
