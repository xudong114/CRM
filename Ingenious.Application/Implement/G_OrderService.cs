using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Enum;
using Ingenious.Infrastructure.Extensions;
using Ingenious.Infrastructure.Helper;
using Ingenious.Repositories;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace Ingenious.Application.Implement
{
    public class G_OrderService : ApplicationService, IG_OrderService
    {
        private readonly IG_OrderRepository _IG_OrderRepository;
        private readonly IG_OrderRecordRepository _IG_OrderRecordRepository;
        private readonly IBase_StoreRepository _IBase_StoreRepository;
        private readonly IBase_StoreService _IBase_StoreService;
        private readonly IG_UserDetailRepository _IG_UserDetailRepository;
        private readonly IG_FileService _IG_FileService;
        public G_OrderService(IRepositoryContext context,
            IG_OrderRepository iG_OrderRepository,
            IBase_StoreRepository iBase_StoreRepository,
            IG_UserDetailRepository iG_UserDetailRepository,
            IG_OrderRecordRepository iG_OrderRecordRepository,
            IG_FileService iG_FileService,
            IBase_StoreService iBase_StoreService)
            : base(context)
        {
            this._IG_OrderRepository = iG_OrderRepository;
            this._IBase_StoreRepository = iBase_StoreRepository;
            this._IG_UserDetailRepository = iG_UserDetailRepository;
            this._IG_OrderRecordRepository = iG_OrderRecordRepository;
            this._IG_FileService = iG_FileService;
            this._IBase_StoreService = iBase_StoreService;
        }

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
        public G_OrderDTOListWithPagination GetAll(int pageIndex = 1, int pageSize = 10,
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
            string sort = "createddate_desc")
        {
            ISpecification<G_Order> spec = Specification<G_Order>.Eval(item => true);

            //订单创建人
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                userId == null || !userId.HasValue || item.CreatedBy == userId.Value));
            //订单申请人姓名
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                name == "" || item.Name == name));
            //订单申请人身份证号码
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                idno == "" || item.IDNo == idno));

            if (statusList != null && statusList.Count() > 0)
            {
                //订单状态
                spec = new AndSpecification<G_Order>(spec,
                    Specification<G_Order>.Eval(item => statusList.Count(s => s == item.Status) > 0));
            }

            //申请贷款银行编码
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                bankCode == null || bankCode == "" || item.BankCode.Equals(bankCode)));
            
            //银行信贷经理工号
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                 bankManager == null || bankManager == "" || item.BankManager.Equals(bankManager)));
            
            //银行客户经理工号
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                 bankClerkCode == null || bankClerkCode == "" || item.BankClerk.Equals(bankClerkCode)));

            //佳居金融经理工号
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                 gojiajumanagercode == null || gojiajumanagercode == "" || item.GoJiajuManagerCode.Equals(gojiajumanagercode)));

            //佳居金融客服工号
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                 gojiajuClerkCode == null || gojiajuClerkCode == "" || item.GoJiajuClerkCode.Equals(gojiajuClerkCode)));


            //关键字：订单编号、申请人姓名、申请人电话等
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                 keyword == null
                    || keyword == ""
                    || keyword == "undefiend"
                    || item.Id.ToString().ToLower().Contains(keyword.ToLower())
                    || item.Code.Contains(keyword)
                    || item.Name.Contains(keyword)
                    || item.PersonalPhone.Contains(keyword)
                 ));

            //申请月份
            spec = new AndSpecification<G_Order>(spec,
                    Specification<G_Order>.Eval(item =>
                     date == null
                     || date == ""
                     || (SqlFunctions.DatePart("year", item.CreatedDate).Value
                        + "-" + (SqlFunctions.DatePart("month", item.CreatedDate).Value < 10
                        ? "0" + SqlFunctions.DatePart("month", item.CreatedDate).Value.ToString()
                        : SqlFunctions.DatePart("month", item.CreatedDate).Value.ToString())) == date
                     ));

            //申请贷款金额范围-最小值
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                 min == null || !min.HasValue || item.LoanAmount >= min.Value));
            //申请贷款金额范围-最大值
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                 max == null || !max.HasValue || item.LoanAmount < max.Value));

            var list = new G_OrderDTOList();

            var result = this._IG_OrderRepository.GetAll(pageIndex, pageSize, spec, sort);

            int totalPages = 0;
            int totalRecords = 0;
            if (result != null)
            {
                result.Data.ForEach(item=>
                    list.Add(Mapper.Map<G_ComplexOrder, G_OrderDTO>(item))
                );
                totalPages = result.TotalPages;
                totalRecords = result.TotalRecords;
            }

            return new G_OrderDTOListWithPagination{
                TotalRecords = totalRecords,
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Data = list
            };
        }

        public System.Linq.Expressions.Expression<Func<G_Order, bool>> ExistsStatus(List<G_OrderStatusEnum> statusList)
        {
            return order => statusList.Count(item => item == order.Status) > 0;//.Select(item => item == order.Status).Count() > 0;
        }

        /// <summary>
        /// 获取订单所有状态的数量
        /// 根据用户id、用户编码和用户类型来查询订单状态数量
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="userCode">当前用户编码</param>
        /// <param name="userType">当前用户类型</param>
        /// <returns></returns>
        public ComplexStatusCount GetComplexStatusCount(Guid userId, string userCode = "", G_UserTypeEnum userType = G_UserTypeEnum.US)
        {
            ISpecification<G_Order> spec = Specification<G_Order>.Eval(item => true);
            switch (userType)
            {
                case G_UserTypeEnum.US://消费者
                    {
                        spec = new AndSpecification<G_Order>(spec,
                           Specification<G_Order>.Eval(item =>
                           item.CreatedBy == userId));
                    }
                    break;
                case G_UserTypeEnum.CL://金融经理
                    {
                        spec = new AndSpecification<G_Order>(spec,
                           Specification<G_Order>.Eval(item =>
                           item.GoJiajuManagerCode == userCode));
                    }
                    break;
                case G_UserTypeEnum.CS://金融客服
                    {

                        spec = new AndSpecification<G_Order>(spec,
                           Specification<G_Order>.Eval(item =>
                           item.GoJiajuClerkCode == userCode));
                    }
                    break;
                case G_UserTypeEnum.BC://银行客户经理
                    {
                        spec = new AndSpecification<G_Order>(spec,
                           Specification<G_Order>.Eval(item =>
                           item.BankClerk == userCode));
                    }
                    break;
                case G_UserTypeEnum.BM://银行信贷经理
                    {
                        spec = new AndSpecification<G_Order>(spec,
                           Specification<G_Order>.Eval(item =>
                           item.BankManager == userCode));
                    }
                    break;
                default:
                    {
                        spec = new AndSpecification<G_Order>(spec,
                            Specification<G_Order>.Eval(item =>
                            item.CreatedBy == userId));
                    }
                    break;
            }

            return this._IG_OrderRepository.GetComplexStatusCount(spec);
        }

        /// <summary>
        /// 获取订单状态的数量
        /// </summary>
        /// <param name="userId">当前账号Id</param>
        /// <param name="status">要查询的订单状态</param>
        /// <returns></returns>
        public int GetStatusCount(Guid userId, G_OrderStatusEnum status)
        {
            ISpecification<G_Order> spec = Specification<G_Order>.Eval(item => true);
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                  item.CreatedBy == userId));
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                  item.Status == status));
            return this._IG_OrderRepository.GetStatusCount(spec);
        }


        /// <summary>
        /// 获取最新成交订单
        /// </summary>
        /// <param name="clerkCode">导购工号</param>
        /// <returns></returns>
        public G_OrderDTO GetLastSuccessOrder(string clerkCode)
        {
            //var list = this.GetAll(clerkCode, null, null, null, G_OrderStatusEnum.Successed, null, null);
            //return list.OrderByDescending(item => item.ModifiedDate).FirstOrDefault() ?? new G_OrderDTO();
            return new G_OrderDTO();
        }

        public G_OrderDTO GetComplexOrder(Guid id)
        {
            ISpecification<G_Order> spec = Specification<G_Order>.Eval(item => true);
            spec = new AndSpecification<G_Order>(spec,
                Specification<G_Order>.Eval(item =>
                 item.Id.Equals(id)));

            var order = this._IG_OrderRepository.GetAll(1, 1, spec).Data.FirstOrDefault();
            if (order == null)
                return null;
            var result = Mapper.Map<G_ComplexOrder, G_OrderDTO>(order);
            var orderRecordList = new List<G_OrderRecordDTO>();

            this._IG_OrderRecordRepository.GetOrderRecordByOrderId(order.Id).OrderByDescending(item => item.CreatedDate).ToList().ForEach(item =>
              orderRecordList.Add(Mapper.Map<G_ComplexOrderRecord, G_OrderRecordDTO>(item))
                );

            //1、订单审核记录
            result.OrderRecordList = orderRecordList;
            //2、申请人所有店铺
            result.Stores = this._IBase_StoreService.GetStoreByCode(result.IDNo);
            
            //订单步骤
            var recordList = new List<G_OrderRecordDTO>();
            if (result.OrderRecordList.Count() > 0)
            {
                var enums = Ingenious.Infrastructure.Enum.G_OrderStatusEnum.BankDenied.ToList<Ingenious.Infrastructure.Enum.G_OrderStatusEnum>();
                //foreach (var e in enums)
                //{
                //    recordList.Add(result.OrderRecordList.Where(item =>
                //    item.Status == e)
                //    .OrderByDescending(item => item.CreatedDate)
                //    .FirstOrDefault());
                //}

                //1、未提交
                recordList.Add(result.OrderRecordList.Where(item =>
                    item.Status ==  G_OrderStatusEnum.Temp)
                    .OrderByDescending(item => item.CreatedDate)
                    .FirstOrDefault());

                //2、平台处理
                recordList.Add(result.OrderRecordList.Where(item =>
                    item.Status == G_OrderStatusEnum.PreDenied
                    || item.Status == G_OrderStatusEnum.PrePassed
                    || item.Status == G_OrderStatusEnum.PreProcess)
                    .OrderByDescending(item => item.CreatedDate)
                    .FirstOrDefault());
                //3、平台分配
                recordList.Add(result.OrderRecordList.Where(item =>
                    item.Status == G_OrderStatusEnum.InProcess
                    || item.Status == G_OrderStatusEnum.GojiajuDenied
                    || item.Status == G_OrderStatusEnum.GojiajuPassed)
                    .OrderByDescending(item => item.CreatedDate)
                    .FirstOrDefault());
                //4、银行审核
                recordList.Add(result.OrderRecordList.Where(item =>
                    item.Status == G_OrderStatusEnum.BankDenied
                    || item.Status == G_OrderStatusEnum.BankPassed)
                    .OrderByDescending(item => item.CreatedDate)
                    .FirstOrDefault());
                //5、银行签约
                recordList.Add(result.OrderRecordList.Where(item =>
                    item.Status == G_OrderStatusEnum.BankSigned
                    || item.Status == G_OrderStatusEnum.SignCanceled)
                    .OrderByDescending(item => item.CreatedDate)
                    .FirstOrDefault());
                //6、放款成功
                recordList.Add(result.OrderRecordList.Where(item =>
                    item.Status == G_OrderStatusEnum.Successed)
                    .OrderByDescending(item => item.CreatedDate)
                    .FirstOrDefault());
                //7、取消
                recordList.Add(result.OrderRecordList.Where(item =>
                    item.Status == G_OrderStatusEnum.Canceled
                    || item.Status == G_OrderStatusEnum.Discard)
                    .OrderByDescending(item => item.CreatedDate)
                    .FirstOrDefault());

                recordList.RemoveAll(i => i == null);
                result.OrderStepRecordList = recordList;
            }
            else
            {
                result.OrderStepRecordList = new List<G_OrderRecordDTO>();
            }
            return result;
        }
        /// <summary>
        /// 分配订单到金融客服
        /// </summary>
        /// <param name="websiteId">所属站点</param>
        /// <returns></returns>
        public string AssignOrderClerk(Guid websiteId)
        {
            var list = this._IG_OrderRepository.AssignOrderClerk(websiteId).ToList();
            if (list.Count() == 0)
            {
                return string.Empty;
            }

            var count = list.Min<KeyValue<string, int>>(i => i.Value);

            var obj = list.Where<KeyValue<string, int>>(item => item.Value == count).FirstOrDefault();

            return obj.Key;
        }

        public DTO.G_OrderDTO GetByKey(System.Guid id)
        {
            var order = this._IG_OrderRepository.GetByKey(id);

            var orderDTO = Mapper.Map<G_Order, G_OrderDTO>(order);
            if (orderDTO != null)
            {
                //var store = this._IF_StoreRepository.GetStoreByCode(orderDTO.StoreCode);
                //if (store != null)
                //{
                //    orderDTO.StoreName = store.Name;
                //}
                //var clerk = this._IF_UserDetailRepository.GetUserDetailByCode(orderDTO.ClerkCode);
                //if (clerk != null)
                //{
                //    orderDTO.ClerkName = clerk.Name;
                //}
            }

            return orderDTO;
        }

        public G_OrderDTO Create(G_OrderDTO dto)
        {
            int count = this._IG_OrderRepository.Data.Where(item => SqlFunctions.DateDiff("day", item.CreatedDate, SqlFunctions.GetDate()) == 0).Count();
            dto.Code = string.Format("JJD{0}{1}", DateTime.Now.ToString("yyyyMMdd"),
                string.Format("{0:D4}", (count + 1)));

            return base.F_Create<G_OrderDTO, G_Order>(dto
                , _IG_OrderRepository
                , dtoAction => { });
        }

        public List<G_OrderDTO> Update(System.Collections.Generic.List<G_OrderDTO> dtoList)
        {
            return base.F_Update<G_OrderDTO, List<G_OrderDTO>, G_Order>(dtoList
                , _IG_OrderRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.ReferenceCode = dto.ReferenceCode;

                    entity.GoJiajuClerkCode = dto.GoJiajuClerkCode;
                    entity.GoJiajuManagerCode = dto.GoJiajuManagerCode;

                    entity.BankCode = dto.BankCode;
                    entity.BankClerk = dto.BankClerk;
                    entity.BankManager = dto.BankManager;

                    entity.FromBank = dto.FromBank;
                    entity.Community = dto.Community;
                    
                    entity.IDNo = dto.IDNo;
                    entity.IDImg = dto.IDImg;
                    entity.IsInstallment = dto.IsInstallment;
                    
                    entity.LoanAmount = dto.LoanAmount;
                    entity.GotLoanAmount = dto.GotLoanAmount;

                    entity.Name = dto.Name;
                    entity.PersonalPhone = dto.PersonalPhone;

                    entity.HasHouse = dto.HasHouse;
                    entity.Province = dto.Province;
                    entity.City = dto.City;
                    entity.Country = dto.Country;
                    entity.Area = dto.Area;
                    entity.Landlord = dto.Landlord;

                    entity.Status = dto.Status;

                    
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public void Delete(System.Collections.Generic.List<G_OrderDTO> dtoList)
        {
            base.F_Update<G_OrderDTO, List<G_OrderDTO>, G_Order>(dtoList
                , _IG_OrderRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

    }
}
