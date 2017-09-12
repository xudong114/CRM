
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_NewsRepository : IRepository<F_News>
    {
        IQueryable<F_News> GetAll(ISpecification<F_News> spec);
    }
}
