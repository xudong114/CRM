
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_ADRepository : IRepository<G_AD>
    {
        IQueryable<G_AD> GetAll(ISpecification<G_AD> spec);
    }
}
