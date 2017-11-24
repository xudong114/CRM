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
    public class Base_StoreRepository : EntityFrameworkRepository<Base_Store>, IBase_StoreRepository
    {
        public Base_StoreRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }


        public IQueryable<Base_Store> GetAll(ISpecification<Base_Store> spec, string sort = "code_desc")
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.Base_Stores.Where(spec.GetExpression());
            switch (sort)
            {
                case "code_desc":
                    {
                        query = query.OrderByDescending(item => item.Code);
                    } break;
                case "code":
                    {
                        query = query.OrderBy(item => item.Code);
                    } break;
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


        public Base_Store GetStoreByCode(string storeCode)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.Base_Stores.Where(item => item.Code.Equals(storeCode));
            return query.FirstOrDefault();
        }

    }
}
