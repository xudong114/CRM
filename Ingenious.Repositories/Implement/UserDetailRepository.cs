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
    public class UserDetailRepository : EntityFrameworkRepository<UserDetail>, IUserDetailRepository
    {
        public UserDetailRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<UserDetail> GetAll(ISpecification<UserDetail> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.UserDetails.Where(spec.GetExpression());
        }

    }
}
