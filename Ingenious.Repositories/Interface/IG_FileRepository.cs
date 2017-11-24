
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_FileRepository : IRepository<G_File>
    {
        IQueryable<G_File> GetAll(ISpecification<G_File> spec);
    }
}
