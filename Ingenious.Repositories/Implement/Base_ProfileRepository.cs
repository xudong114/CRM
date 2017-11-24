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
    public class Base_ProfileRepository : EntityFrameworkRepository<Base_Profile>, IBase_ProfileRepository
    {
        public Base_ProfileRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public Base_Profile GetProfileByIDNo(string idNo)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            return context.Base_Profile.Where(item => item.IDNo.Equals(idNo)).FirstOrDefault();
        }

        public PagedResult<Base_Profile> GetAll(int pageIndex, int pageSize, ISpecification<Base_Profile> spec, string sort = "createddate_desc")
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            var query = from o in context.Base_Profile.Where(spec.GetExpression())
                        select o;

            switch (sort)
            {
                case "createddate_desc":
                    {
                        query = query.OrderByDescending(item => item.CreatedDate);
                    }
                    break;
                case "createddate":
                    {
                        query = query.OrderBy(item => item.CreatedDate);
                    }
                    break;
                default:
                    {
                        query = query.OrderByDescending(item => item.CreatedDate);
                    }
                    break;
            }

            int skip;
            try
            {
                skip = checked((pageIndex - 1) * pageSize);
            }
            catch (OverflowException)
            {
                skip = 0;
            }

            int take = pageSize;
            var pagedQuery = query.Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
            if (pagedQuery == null)
                return new PagedResult<Base_Profile>();
            return new PagedResult<Base_Profile>(pagedQuery.Key.Total, (pagedQuery.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedQuery.Select(p => p).ToList());
        }
    }
}
