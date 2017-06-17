
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User Login(User user);
        IQueryable<User> GetAll(ISpecification<User> spec);

    }
}
