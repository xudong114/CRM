using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Interface
{
    public interface IContractRepository : IRepository<Contract>
    {
        IQueryable<Contract> GetAll(ISpecification<Contract> spec);
        Contract Create(Contract contract);
    }
}
