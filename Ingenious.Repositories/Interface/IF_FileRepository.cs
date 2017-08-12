
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_FileRepository : IRepository<F_File>
    {
        IQueryable<F_File> GetAll(ISpecification<F_File> spec);
    }
}
