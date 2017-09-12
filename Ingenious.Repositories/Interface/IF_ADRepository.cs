
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_ADRepository : IRepository<F_AD>
    {
        IQueryable<F_AD> GetAll(ISpecification<F_AD> spec);
    }
}
