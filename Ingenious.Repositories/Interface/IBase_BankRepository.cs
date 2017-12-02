
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IBase_BankRepository : IRepository<Base_Bank>
    {
        IQueryable<Base_Bank> GetAll(int pageIndex, int pageSize, ISpecification<Base_Bank> spec, string sort = "order");
        int GetMaxCode();
    }
}
