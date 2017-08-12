
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_ActivityRepository : IRepository<F_Activity>
    {
        IQueryable<F_Activity> GetAll(ISpecification<F_Activity> spec);
    }
}
