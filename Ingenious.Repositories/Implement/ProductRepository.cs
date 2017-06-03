using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System.Linq;

namespace Ingenious.Repositories.Implement
{
    public class ProductRepository : EntityFrameworkRepository<Product>, IProductRepository
    {
        public ProductRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Product> GetAll(ISpecification<Product> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Products.Where(spec.GetExpression());
        }

    }
}
