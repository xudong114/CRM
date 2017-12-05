using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Infrastructure;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IBase_CarRepository : IRepository<Base_Car>
    {
        /// <summary>
        /// 根据身份证号码获取车辆信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        IQueryable<Base_Car> GetCarByIDNo(string idNo);
    }
}
