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
    public class G_ADRepository : EntityFrameworkRepository<G_AD>, IG_ADRepository
    {
        public G_ADRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<G_AD> GetAll(ISpecification<G_AD> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.G_ADs.Where(spec.GetExpression());
        }

    }
}
