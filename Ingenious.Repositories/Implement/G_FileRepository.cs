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
    public class G_FileRepository : EntityFrameworkRepository<G_File>, IG_FileRepository
    {
        public G_FileRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<G_File> GetAll(ISpecification<G_File> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.G_Files.Where(spec.GetExpression());
        }

    }
}
