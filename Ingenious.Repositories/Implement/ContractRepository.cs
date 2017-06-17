using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ingenious.Repositories.Implement
{
    public class ContractRepository : EntityFrameworkRepository<Contract>, IContractRepository
    {
        public ContractRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Contract> GetAll(ISpecification<Contract> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Contracts.Where(spec.GetExpression());
        }

        public Contract Create(Contract contract)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            using (TransactionScope ts = new TransactionScope())
            {

                context.Contracts.Add(contract);
                context.SaveChanges();

                //账户
                var account = context.Accounts.First(model => model.DepartmentId == contract.DepartmentId);
                account.Balance -= contract.TotalAmount;
                context.Accounts.Attach(account);
                context.Entry(account).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                //账单
                //var bill = new Bill();
                var bill = context.Bills.Create();
                bill.CreatedBy = bill.CreatedBy = contract.CreatedBy;
                bill.AccountId = account.Id;
                bill.ContractId = contract.Id;
                bill.Money = contract.TotalAmount;
                bill.Version = 1;
                bill.IsActive = true;
                bill.CreatedDate = bill.ModifiedDate = DateTime.Now;
                //context.Bills.Attach(bill);
                //context.Entry(bill).State = System.Data.Entity.EntityState.Added;
                context.Bills.Add(bill);
                context.SaveChanges();

                ts.Complete();
            }

            return contract;
        }

    }
}
