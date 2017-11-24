
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IBase_StoreRepository : IRepository<Base_Store>
    {
        IQueryable<Base_Store> GetAll(ISpecification<Base_Store> spec, string sort = "code_desc");

        Base_Store GetStoreByCode(string storeCode);
    }
}
