
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_WithdrawDepositRecordRepository : IRepository<F_WithdrawDepositRecord>
    {
        IQueryable<F_WithdrawDepositRecord> GetAll(ISpecification<F_WithdrawDepositRecord> specification);
    }
}
