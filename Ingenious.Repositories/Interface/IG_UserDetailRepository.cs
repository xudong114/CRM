
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_UserDetailRepository : IRepository<G_UserDetail>
    {
        IQueryable<G_UserDetail> GetAll(ISpecification<G_UserDetail> spec);

        G_UserDetail GetUserDetailByCode(string code);

    }
}
