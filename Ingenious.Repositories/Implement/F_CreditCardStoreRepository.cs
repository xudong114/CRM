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
    public class F_CreditCardStoreRepository : EntityFrameworkRepository<F_CreditCardStore>, IF_CreditCardStoreRepository
    {
        public F_CreditCardStoreRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_CreditCardStore> GetAll(ISpecification<F_CreditCardStore> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_CreditCardStores.Where(spec.GetExpression());
        }

    }
}
