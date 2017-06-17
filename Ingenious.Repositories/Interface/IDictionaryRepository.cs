using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Interface
{
    public interface IDictionaryRepository : IRepository<Dictionary>
    {
        Dictionary GetByCode(string code);
        Dictionary GetByName(string name);
        IQueryable<Dictionary> GetAll(ISpecification<Dictionary> spec);
    }
}
