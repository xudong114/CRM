
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_LoanProductRepository : IRepository<G_LoanProduct>
    {
        IQueryable<G_LoanProduct> GetAll(ISpecification<G_LoanProduct> spec, string sort = "code_desc");

    }
}
