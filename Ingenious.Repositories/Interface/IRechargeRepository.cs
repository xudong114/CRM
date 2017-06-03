using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Interface
{
    public interface IRechargeRepository : IRepository<Recharge>
    {
        IQueryable<Recharge> GetByAccountId(Guid id);
        IQueryable<Recharge> GetAll(ISpecification<Recharge> spec);
    }
}
