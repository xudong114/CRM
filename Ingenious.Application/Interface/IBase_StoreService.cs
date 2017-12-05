using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IBase_StoreService : IApplication<Base_StoreDTO>
    {
        Base_StoreDTO GetStoreByName(string name);

        /// <summary>
        /// 根据店铺编码（Code）、身份证号码、手机号码、营业执照号码查询店铺
        /// </summary>
        /// <param name="code">店铺编码（Code）、身份证号码、手机号码、营业执照号码</param>
        /// <returns></returns>
        List<Base_StoreDTO> GetStoreByCode(string code);

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
        /// <returns>Base_StoreDTO</returns>
        Base_StoreDTO GetStoreByClerkId(string userCode);
        /// <summary>
        /// 根据店铺英文名称获取店铺资料
        /// </summary>
        /// <param name="englishName">店铺英文名称</param>
        /// <returns></returns>
        Base_StoreDTO GetStoreByEnglishName(string englishName);
        /// <summary>
        /// 根据店铺编码获取导购
        /// </summary>
        /// <param name="storeCode">店铺编号</param>
        /// <returns></returns>
        List<F_UserDetailDTO> GetClerks(string storeCode);

        /// <summary>
        /// 获取店铺
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="websiteId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<Base_StoreDTO> GetStores(bool? isActive = null, string websiteId = "", DateTime? beginDate = null, DateTime? endDate = null, string sort = "code_desc");
    }
}
