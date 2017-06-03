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
    public class UserRepository : EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public User Login(User user)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            return context.Users.Where(item=>item.UserName.ToLower().Equals(user.UserName.ToLower()) 
                && item.Password.Equals(user.Password)).FirstOrDefault();
        }

        public IQueryable<User> GetAll(ISpecification<User> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Users.Where(spec.GetExpression());
        }

    }
}
