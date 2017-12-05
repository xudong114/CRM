using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IBase_CarService : IApplication<Base_CarDTO>
    {
        /// <summary>
        /// 根据身份证号码获取车辆信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        Base_CarDTOList GetCarByIDNo(string idNo);
    }
}
