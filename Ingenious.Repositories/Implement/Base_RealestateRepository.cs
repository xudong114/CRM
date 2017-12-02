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
    public class Base_RealestateRepository : EntityFrameworkRepository<Base_Realestate>, IBase_RealestateRepository
    {
        public Base_RealestateRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Base_Realestate> GetRealestateByIDNo(string idNo)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            return context.Base_Realestates.Where(item => item.IDNo.Equals(idNo));
        }

    }
}
