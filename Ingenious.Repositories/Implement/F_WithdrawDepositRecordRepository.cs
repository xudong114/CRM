using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class F_WithdrawDepositRecordRepository : EntityFrameworkRepository<F_WithdrawDepositRecord>, IF_WithdrawDepositRecordRepository
    {
        public F_WithdrawDepositRecordRepository(IRepositoryContext context) 
            : base(context) 
        {
        }


        public IQueryable<F_WithdrawDepositRecord> GetAll(ISpecification<F_WithdrawDepositRecord> specification)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_WithdrawDepositRecords.Where(specification.GetExpression());
        }
    }
}
