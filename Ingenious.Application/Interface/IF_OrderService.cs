﻿using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_OrderService : IApplication<F_OrderDTO>
    {
        F_OrderDTOListWithPagination GetAll(int pageIndex, int pageSize,
             string clerkCode = "",
             string storeCode = "",
             string bankCode = null,
             Guid? userId = null,
             F_OrderStatusEnum? status = null,
             string bankManager = "",
             string bankClerkCode = "",
             string gojiajuClerkCode = "",
             string keyword = "",
             string date = "",
             decimal? min = null,
             decimal? max = null,
            bool? isActive = null,
             string sort = "createddate_desc");
        F_OrderDTO GetLastSuccessOrder(string clerkCode);

        F_OrderDTO GetComplexOrder(Guid id);
        string AssignOrderClerk(Guid websiteId);

    }
}
