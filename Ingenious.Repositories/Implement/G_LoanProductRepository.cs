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
    public class G_LoanProductRepository : EntityFrameworkRepository<G_LoanProduct>, IG_LoanProductRepository
    {
        public G_LoanProductRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<G_LoanProduct> GetAll(ISpecification<G_LoanProduct> spec, string sort = "code_desc")
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.G_LoanProducts.Where(spec.GetExpression());
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
