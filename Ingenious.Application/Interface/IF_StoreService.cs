using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_StoreService : IApplication<F_StoreDTO>
    {
        F_StoreDTO GetStoreByName(string name);
        F_StoreDTO GetStoreByCode(string code);

        /// <summary>
        /// 绑定店员到店铺
        /// </summary>
        /// <param name="storeCode">店铺编号</param>
        /// <param name="userCode">导购工号</param>
        /// <returns></returns>
        bool BindStore(string storeCode, string userCode);
        /// <summary>
        /// 根据导购工号获取店铺
        /// </summary>
        /// <param name="userCode">导购工号</param>
        /// <returns>F_StoreDTO</returns>
        F_StoreDTO GetStoreByClerkId(string userCode);
        /// <summary>
        /// 根据店铺英文名称获取店铺资料
        /// </summary>
        /// <param name="englishName">店铺英文名称</param>
        /// <returns></returns>
        F_StoreDTO GetStoreByEnglishName(string englishName);
        /// <summary>
        /// 根据店铺编码获取导购
        /// </summary>
        /// <param name="storeCode">店铺编号</param>
        /// <returns></returns>
        List<F_UserDetailDTO> GetClerks(string storeCode);
    }
}
