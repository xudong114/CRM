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
    public class G_NewsRepository : EntityFrameworkRepository<G_News>, IG_NewsRepository
    {
        public G_NewsRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<G_News> GetAll(ISpecification<G_News> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.G_News.Where(spec.GetExpression());
        }

    }
}
