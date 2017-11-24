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
    public class G_UserRepository : EntityFrameworkRepository<G_User>, IG_UserRepository
    {
        public G_UserRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public G_User Login(G_User user)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            return context.G_Users.Where(item=>item.UserName.ToLower().Equals(user.UserName.ToLower()) 
                && item.Password.Equals(user.Password)).FirstOrDefault();
        }

        public IQueryable<G_User> GetAll(ISpecification<G_User> spec, string sort = "createddate_desc")
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.G_Users.Where(spec.GetExpression());
            switch (sort)
            {
                case "createddate_desc":
                    {
                        query = query.OrderByDescending(item => item.CreatedDate);
                    } break;
                case "createddate":
                    {
                        query = query.OrderBy(item => item.CreatedDate);
                    } break;
            }
            return query;
        }

        public IQueryable<G_User> GetUserByBankCode(string bankCode)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from u in context.G_Users
                        join ud in context.G_UserDetails on u.Id equals ud.G_UserId
                        where ud.BankCode == bankCode
                        select u;
            return query;
        }

        public int Update(G_User user)
        {
            var sql = string.Format("update G_User set G_EntityId = '{0}' where id = '{1}' ", user.G_EntityId, user.Id);
            return  this.EFContext.Context.Database.ExecuteSqlCommand(sql);
        }


        public G_User GetUserByUserName(string username)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from u in context.G_Users
                        where u.UserName == username
                        select u;
            return query.FirstOrDefault();
        }
    }
}
