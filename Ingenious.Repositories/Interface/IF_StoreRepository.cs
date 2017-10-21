
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_StoreRepository : IRepository<F_Store>
    {
        IQueryable<F_Store> GetAll(ISpecification<F_Store> spec, string sort = "code_desc");
        bool BindStore(string storeCode, string userCode);
        F_Store GetStoreByClerkId(string userCode);
        F_Store GetStoreByCode(string storeCode);
        IQueryable<F_UserDetail> GetClerks(string storeCode);
    }
}
