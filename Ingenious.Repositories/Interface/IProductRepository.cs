using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetAll(ISpecification<Product> spec);
    }
}
