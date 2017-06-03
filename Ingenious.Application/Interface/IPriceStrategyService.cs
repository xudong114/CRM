using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IPriceStrategyService : IApplication<PriceStrategyDTO>
    {
        PriceStrategyDTOList GetAll(string keywords = "");
        PriceStrategyDTO GetPriceStrategyByUserId(Guid id);
    }
}
