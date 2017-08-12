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
    public class F_UserRepository : EntityFrameworkRepository<F_User>, IF_UserRepository
    {
        public F_UserRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public F_User Login(F_User user)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            return context.F_Users.Where(item=>item.UserName.ToLower().Equals(user.UserName.ToLower()) 
                && item.Password.Equals(user.Password)).FirstOrDefault();
        }

        public IQueryable<F_User> GetAll(ISpecification<F_User> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_Users.Where(spec.GetExpression());
        }

        public IQueryable<F_User> GetUserByBankCode(string bankCode)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from u in context.F_Users
                        join ud in context.F_UserDetails on u.Id equals ud.F_UserId
                        where ud.BankCode == bankCode
                        select u;
            return query;
        }
    }
}
