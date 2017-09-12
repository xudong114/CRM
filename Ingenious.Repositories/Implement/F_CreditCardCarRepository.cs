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
    public class F_CreditCardCarRepository : EntityFrameworkRepository<F_CreditCardCar>, IF_CreditCardCarRepository
    {
        public F_CreditCardCarRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_CreditCardCar> GetAll(ISpecification<F_CreditCardCar> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_CreditCardCars.Where(spec.GetExpression());
        }

    }
}
