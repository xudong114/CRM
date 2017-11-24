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
    public class Base_CarRepository : EntityFrameworkRepository<Base_Car>, IBase_CarRepository
    {
        public Base_CarRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Base_Car> GetCarByIDNo(string idNo)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            return context.Base_Car.Where(item => item.IDNo.Equals(idNo));
        }

    }
}
