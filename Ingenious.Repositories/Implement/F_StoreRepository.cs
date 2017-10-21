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
    public class F_StoreRepository : EntityFrameworkRepository<F_Store>, IF_StoreRepository
    {
        public F_StoreRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_Store> GetAll(ISpecification<F_Store> spec, string sort = "code_desc")
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.F_Stores.Where(spec.GetExpression());
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

        public bool BindStore(string storeCode, string userCode)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var model = new F_StoreClerk
            {
                CreatedBy = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                IsActive = true,
                ModifiedBy = Guid.NewGuid(),
                ModifiedDate = DateTime.Now,
                StoreCode = storeCode,
                UserCode = userCode,
                Version = 1
            };
            context.F_StoreClerks.Add(model);

            return context.SaveChanges() > 0;
        }


        public F_Store GetStoreByClerkId(string userCode)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from s in context.F_Stores
                        join sc in context.F_StoreClerks on s.Code equals sc.StoreCode //into scdata
                        select s;
            return query.FirstOrDefault();
        }

        public F_Store GetStoreByCode(string storeCode)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = context.F_Stores.Where(item => item.Code.Equals(storeCode));
            return query.FirstOrDefault();
        }

        public IQueryable<F_UserDetail> GetClerks(string storeCode)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from s in context.F_Stores
                        join sc in context.F_StoreClerks on s.Code equals sc.StoreCode //into scdata
                        join u in context.F_UserDetails on sc.UserCode equals u.Code
                        select u;
            return query;
        }

    }
}
