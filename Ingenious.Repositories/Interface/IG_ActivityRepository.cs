using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Infrastructure;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_ActivityRepository : IRepository<G_Activity>
    {
        PagedResult<G_Activity> GetAll(int pageIndex, int pageSize, ISpecification<G_Activity> spec, string sort = "createddate_desc");
    }
}
