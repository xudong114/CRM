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
    public class F_CreditCardRepository : EntityFrameworkRepository<F_CreditCard>, IF_CreditCardRepository
    {
        public F_CreditCardRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_CreditCard> GetAll(ISpecification<F_CreditCard> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_CreditCards.Where(spec.GetExpression());
        }

    }
}
