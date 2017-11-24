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
    public class G_UserDetailRepository : EntityFrameworkRepository<G_UserDetail>, IG_UserDetailRepository
    {
        public G_UserDetailRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<G_UserDetail> GetAll(ISpecification<G_UserDetail> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.G_UserDetails.Where(spec.GetExpression());
        }

        public G_UserDetail GetUserDetailByCode(string code)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.G_UserDetails.Where(item=>item.Code.Equals(code)).FirstOrDefault();
        }

    }
}
