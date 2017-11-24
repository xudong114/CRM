
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_UserRepository : IRepository<G_User>
    {
        G_User Login(G_User user);
        IQueryable<G_User> GetAll(ISpecification<G_User> spec,string sort = "createddate_desc");

        IQueryable<G_User> GetUserByBankCode(string bankCode);

        int Update(G_User user);
        /// <summary>
        /// 根据用户名获取账号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        G_User GetUserByUserName(string username);
    }
}
