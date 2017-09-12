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
    public class F_CreditCardHouseRepository : EntityFrameworkRepository<F_CreditCardHouse>, IF_CreditCardHouseRepository
    {
        public F_CreditCardHouseRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_CreditCardHouse> GetAll(ISpecification<F_CreditCardHouse> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_CreditCardHouses.Where(spec.GetExpression());
        }

    }
}
