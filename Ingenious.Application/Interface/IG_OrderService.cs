using Ingenious.Domain.DataSource;
using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_OrderService : IApplication<G_OrderDTO>
    {
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页显示记录数量</param>
        /// <param name="userId">订单创建人Id</param>
        /// <param name="statusList">订单状态列表</param>
        /// <param name="productCode">贷款产品编码</param>
        /// <param name="date">申请月份</param>
        /// <param name="keyword">订单编号、申请人姓名、申请人电话等</param>
        /// <param name="bankCode">申请贷款银行编码</param>
        /// <param name="bankManager">银行信贷经理工号</param>
        /// <param name="bankClerkCode">银行客户经理工号</param>
        /// <param name="gojiajumanagercode">Go佳居金融经理工号</param>
        /// <param name="gojiajuClerkCode">Go佳居金融客服工号</param>
        /// <param name="gojiajuSellerCode">Go佳居金融专员工号</param>
        /// <param name="name">订单申请人姓名</param>
        /// <param name="idno">订单申请人身份证号码</param>
        /// <param name="storeCode">订单申请人关联店铺编码</param>
        /// <param name="min">申请贷款金额范围-最小值</param>
        /// <param name="max">申请贷金额款范围-最大值</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        G_OrderDTOListWithPagination GetAll(int pageIndex = 1, int pageSize = 10,
            Guid? userId = null,
            List<G_OrderStatusEnum> statusList = default(List<G_OrderStatusEnum>),
            string productCode = "",
            string date = "",
            string keyword = "",
            string bankCode = null,
            string bankManager = "",
            string bankClerkCode = "",
            string gojiajumanagercode = "",
            string gojiajuClerkCode = "",
            string gojiajuSellerCode = "",
            string name = "",
            string idno = "",
            string storeCode = "",
            decimal? min = null,
            decimal? max = null,
            string sort = "createddate_desc");

        /// <summary>
        /// 获取订单所有状态的数量
        /// 根据用户id、用户编码和用户类型来查询订单状态数量
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="userCode">当前用户编码</param>
        /// <param name="userType">当前用户类型</param>
        /// <returns></returns>
        ComplexStatusCount GetComplexStatusCount(Guid userId, string userCode = "", G_UserTypeEnum userType = G_UserTypeEnum.US);

        /// <summary>
        /// 获取订单状态的数量
        /// </summary>
        /// <param name="userId">当前账号Id</param>
        /// <param name="status">要查询的订单状态</param>
        /// <returns></returns>
        int GetStatusCount(Guid userId, G_OrderStatusEnum status);

        G_OrderDTO GetLastSuccessOrder(string clerkCode);

        G_OrderDTO GetComplexOrder(Guid id);
        string AssignOrderClerk(Guid websiteId);

    }
}
