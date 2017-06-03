using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{

    public interface IUserDetailRepository : IRepository<UserDetail>
    {
        IQueryable<UserDetail> GetAll(ISpecification<UserDetail> spec);
    }

}
