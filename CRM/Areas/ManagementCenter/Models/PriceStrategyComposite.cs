using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Areas.ManagementCenter.Models
{
    public class PriceStrategyComposite
    {
        public PriceStrategyDTO PriceStrategyDTO { get; set; }

        public ProductDTOList ProductDTOList { get; set; }

        public List<PriceStrategyDetailDTO> PriceStrategyDetailDTOList { get; set; }

    }
}