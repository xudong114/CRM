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
    public class Base_FactoryRepository : EntityFrameworkRepository<Base_Factory>, IBase_FactoryRepository
    {
        public Base_FactoryRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<Base_Factory> GetAll(ISpecification<Base_Factory> spec, string sort = "code_desc")
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.Base_Factorys.Where(spec.GetExpression());
            switch (sort)
            {
                case "createddate_desc":
                    {
                        query = query.OrderByDescending(item => item.CreatedDate);
                    } break;
                case "createddate":
                    {
                        query = query.OrderBy(item => item.CreatedDate);
                    } break;
            }
            return query;
        }

    }
}
