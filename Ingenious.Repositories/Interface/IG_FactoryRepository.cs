
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IBase_FactoryRepository : IRepository<Base_Factory>
    {
        IQueryable<Base_Factory> GetAll(ISpecification<Base_Factory> spec, string sort = "code_desc");

    }
}
