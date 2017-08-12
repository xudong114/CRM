
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_UserDetailRepository : IRepository<F_UserDetail>
    {
        IQueryable<F_UserDetail> GetAll(ISpecification<F_UserDetail> spec);

        F_UserDetail GetUserDetailByCode(string code);

    }
}
