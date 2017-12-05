using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_UserService : IApplication<G_UserDTO>
    {
        G_UserDTO Login(G_UserDTO user);
        G_UserDTO GetUserByUserName(string userName);
        G_UserDTOList GetUsers(bool? isActive = true, string keywords = "", G_UserTypeEnum? userType = null, string order = "createddate_desc");
        G_UserDTOList GetBankUser(string bankcode = "", bool? isactive = null);

        /// <summary>
        /// 根据推广码获取金融客服
        /// </summary>
        /// <param name="referenceCode">推广码</param>
        /// <returns></returns>
        G_UserDTO GetGojiajuClerkByReferenceCode(string referenceCode);

        /// <summary>
        /// 根据推广码获取金融经理
        /// </summary>
        /// <param name="entityId">机构标识</param>
        /// <returns></returns>
        G_UserDTO GetGojiajuManagerByEntityId(Guid entityId);
    }
}
