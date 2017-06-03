using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IActivityCategoryRepository : IRepository<ActivityCategory>
    {
        IQueryable<ActivityCategory> GetAll();

    }
}
