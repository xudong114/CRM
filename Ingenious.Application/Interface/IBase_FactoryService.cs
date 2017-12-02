using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IBase_FactoryService : IApplication<Base_FactoryDTO>
    {
        /// <summary>
        /// 根据身份证号码获取工厂信息
        /// </summary>
        /// <param name="idno">身份证号码</param>
        /// <returns></returns>
        Base_FactoryDTOList GetFactoryByIDNo(string idno);
        /// <summary>
        /// 根据身份证号码、营业执照编号来查询工厂
        /// </summary>
        /// <param name="code">手机号码、身份证号码、营业执照编号</param>
        /// <returns></returns>
        Base_FactoryDTO GetFactoryByCode(string code);

    }
}
