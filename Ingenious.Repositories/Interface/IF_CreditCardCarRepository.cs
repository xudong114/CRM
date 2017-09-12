
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_CreditCardCarRepository : IRepository<F_CreditCardCar>
    {
        IQueryable<F_CreditCardCar> GetAll(ISpecification<F_CreditCardCar> spec);
    }
}
