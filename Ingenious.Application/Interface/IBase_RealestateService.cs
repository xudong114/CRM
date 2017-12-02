using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IBase_RealestateService : IApplication<Base_RealestateDTO>
    {
        /// <summary>
        /// 根据身份证号码获取车辆信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        Base_RealestateDTOList GetRealestateByIDNo(string idNo);
    }
}
