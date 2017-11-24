
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_NewsRepository : IRepository<G_News>
    {
        IQueryable<G_News> GetAll(ISpecification<G_News> spec);
    }
}
