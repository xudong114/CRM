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
    public class F_CreditCardFileRepository : EntityFrameworkRepository<F_CreditCardFile>, IF_CreditCardFileRepository
    {
        public F_CreditCardFileRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_CreditCardFile> GetAll(ISpecification<F_CreditCardFile> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_CreditCardFiles.Where(spec.GetExpression());
        }

    }
}
