using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Infrastructure;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class Base_AccountRepository : EntityFrameworkRepository<Base_Account>, IBase_AccountRepository
    {
        public Base_AccountRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Base_Account> GetAccountByIDNo(string idNo)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            return context.Base_Accounts.Where(item => item.IDNo.Equals(idNo));
        }

    }
}
