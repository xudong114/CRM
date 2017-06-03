using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System.Linq;

namespace Ingenious.Repositories.Implement
{
    public class DepartmentRepository : EntityFrameworkRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Department> GetAll(ISpecification<Department> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Departments.Where(spec.GetExpression());
        }

    }
}
