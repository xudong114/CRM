using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IActivityRepository : IRepository<Activity>
    {
        IQueryable<Activity> GetAll(ISpecification<Activity> spec);

    }
}
