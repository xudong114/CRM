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
    public class F_FileRepository : EntityFrameworkRepository<F_File>, IF_FileRepository
    {
        public F_FileRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_File> GetAll(ISpecification<F_File> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_Files.Where(spec.GetExpression());
        }

    }
}
