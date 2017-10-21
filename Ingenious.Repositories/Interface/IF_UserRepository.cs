
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_UserRepository : IRepository<F_User>
    {
        F_User Login(F_User user);
        IQueryable<F_User> GetAll(ISpecification<F_User> spec,string sort = "createddate_desc");

        IQueryable<F_User> GetUserByBankCode(string bankCode);
    }
}
