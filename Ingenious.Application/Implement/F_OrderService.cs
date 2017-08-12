using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Enum;
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
    public class F_OrderService : ApplicationService, IF_OrderService
    {
        private readonly IF_OrderRepository _IF_OrderRepository;
        private readonly IF_OrderRecordRepository _IF_OrderRecordRepository;
        private readonly IF_StoreRepository _IF_StoreRepository;
        private readonly IF_UserDetailRepository _IF_UserDetailRepository;
        private readonly IF_FileService _IF_FileService;
        public F_OrderService(IRepositoryContext context,
            IF_OrderRepository iF_OrderRepository,
            IF_StoreRepository iF_StoreRepository,
            IF_UserDetailRepository iF_UserDetailRepository,
            IF_OrderRecordRepository iF_OrderRecordRepository,
            IF_FileService iF_FileService)
            : base(context)
        {
            this._IF_OrderRepository = iF_OrderRepository;
            this._IF_StoreRepository = iF_StoreRepository;
            this._IF_UserDetailRepository = iF_UserDetailRepository;
            this._IF_OrderRecordRepository = iF_OrderRecordRepository;
            this._IF_FileService = iF_FileService;
        }

        public F_OrderDTOList GetAll(
            string clerkCode = "",
            string storeCode = "",
            string bankCode = null,
            Guid? userId = null,
            F_OrderStatusEnum? status = null,
            string bankManager = "",
            string bankClerkCode = "",
            string gojiajuClerkCode = "",
            decimal? min = null,
            decimal? max = null)
        {
            ISpecification<F_Order> spec = Specification<F_Order>.Eval(item => true);
            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                  clerkCode == null || clerkCode == "" || item.ClerkCode.Equals(clerkCode)));

            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                 storeCode == null || storeCode == "" || item.StoreCode.Equals(storeCode)));

            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                bankCode == null || bankCode == "" || item.BankCode.Equals(bankCode)));

            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                userId == null || !userId.HasValue || item.CreatedBy.Equals(userId.Value)));

            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                 status == null || !status.HasValue || item.Status == status.Value));

            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                 bankManager == null || bankManager == "" || item.BankManager.Equals(bankManager)));

            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                 bankClerkCode == null || bankClerkCode == "" || item.BankClerk.Equals(bankClerkCode)));

            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                 gojiajuClerkCode == null || gojiajuClerkCode == "" || item.GoJiajuClerkCode.Equals(gojiajuClerkCode)));

            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                 min == null || !min.HasValue || item.LoanAmount >= min.Value));

            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                 max == null || !max.HasValue || item.LoanAmount < max.Value));
                        spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                 bankClerkCode == null || bankClerkCode == "" || item.BankClerk.Equals(bankClerkCode)));


            var list = new F_OrderDTOList();

            this._IF_OrderRepository.GetAll(spec).OrderByDescending(item=>item.CreatedDate).ToList().ForEach(item =>
                {
                    list.Add(Mapper.Map<ComplexOrder, F_OrderDTO>(item));
                });
            return list;
        }
        /// <summary>
        /// 获取最新成交订单
        /// </summary>
        /// <param name="clerkCode">导购工号</param>
        /// <returns></returns>
        public F_OrderDTO GetLastSuccessOrder(string clerkCode)
        {
            var list = this.GetAll(clerkCode, null, null, null, F_OrderStatusEnum.Successed, null, null);
            return list.OrderByDescending(item => item.ModifiedDate).FirstOrDefault() ?? new F_OrderDTO();
        }

        public F_OrderDTO GetComplexOrder(Guid id)
        {
            ISpecification<F_Order> spec = Specification<F_Order>.Eval(item => true);
            spec = new AndSpecification<F_Order>(spec,
                Specification<F_Order>.Eval(item =>
                 item.Id.Equals(id)));

            var order = this._IF_OrderRepository.GetAll(spec).ToList().FirstOrDefault();
            if (order == null)
                return null;
            var result = Mapper.Map<ComplexOrder, F_OrderDTO>(order);
            var orderRecordList = new List<F_OrderRecordDTO>();

            this._IF_OrderRecordRepository.GetOrderRecordByOrderId(order.Id).OrderByDescending(item=>item.CreatedDate).ToList().ForEach(item =>
              orderRecordList.Add(Mapper.Map<ComplexOrderRecord, F_OrderRecordDTO>(item))
                );
            result.OrderRecordList = orderRecordList;
            result.Files = this._IF_FileService.GetFilesByReferenceId(result.Id);
            return result;
        }
        /// <summary>
        /// 分配订单到金融客服
        /// </summary>
        /// <param name="websiteId">所属站点</param>
        /// <returns></returns>
        public string AssignOrderClerk(Guid websiteId)
        {
            var list = this._IF_OrderRepository.AssignOrderClerk(websiteId).ToList();
            if (list.Count() == 0)
            {
                return string.Empty;
            }

            var count = list.Min<KeyValue<string, int>>(i => i.Value);

            var obj = list.Where<KeyValue<string, int>>(item => item.Value == count).FirstOrDefault();

            return obj.Key;
        }

        public DTO.F_OrderDTO GetByKey(System.Guid id)
        {
            var order = this._IF_OrderRepository.GetByKey(id);

            var orderDTO = Mapper.Map<F_Order, F_OrderDTO>(order);
            if (orderDTO != null)
            {
                var store = this._IF_StoreRepository.GetStoreByCode(orderDTO.StoreCode);
                if (store != null)
                {
                    orderDTO.StoreName = store.Name;
                }
                var clerk = this._IF_UserDetailRepository.GetUserDetailByCode(orderDTO.ClerkCode);
                if (clerk != null)
                {
                    orderDTO.ClerkName = clerk.Name;
                }
            }

            return orderDTO;
        }

        public F_OrderDTO Create(F_OrderDTO dto)
        {
            int count = this._IF_OrderRepository.Data.Where(item => SqlFunctions.DateDiff("day", item.CreatedDate, SqlFunctions.GetDate()) == 0).Count();
            dto.Code = string.Format("GFQ{0}{1}", DateTime.Now.ToString("yyyyMMdd"),
                string.Format("{0:D4}", (count + 1)));

            return base.F_Create<F_OrderDTO, F_Order>(dto
                , _IF_OrderRepository
                , dtoAction => { });
        }

        public List<F_OrderDTO> Update(System.Collections.Generic.List<F_OrderDTO> dtoList)
        {
            return base.F_Update<F_OrderDTO, List<F_OrderDTO>, F_Order>(dtoList
                , _IF_OrderRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.BankCode = dto.BankCode;
                    entity.BankClerk = dto.BankClerk;
                    entity.GoJiajuClerkCode = dto.GoJiajuClerkCode;
                    entity.BankManager = dto.BankManager;

                    entity.FromBank = dto.FromBank;
                    entity.City = dto.City;
                    entity.ClerkCode = dto.ClerkCode;
                    entity.Community = dto.Community;
                    entity.Country = dto.Country;
                    entity.DownpaymentAmount = dto.DownpaymentAmount;
                    entity.IDNo = dto.IDNo;
                    entity.IsInstallment = dto.IsInstallment;
                    entity.Landlord = dto.Landlord;
                    entity.LoanAmount = dto.LoanAmount;
                    entity.Name = dto.Name;
                    entity.PersonalPhone = dto.PersonalPhone;
                    entity.Province = dto.Province;
                    entity.Status = dto.Status;
                    entity.StoreCode = dto.StoreCode;
                    entity.TotalAmount = dto.TotalAmount;
                    entity.GotLoanAmount = dto.GotLoanAmount;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<F_OrderDTO> dtoList)
        {
            base.F_Update<F_OrderDTO, List<F_OrderDTO>, F_Order>(dtoList
                , _IF_OrderRepository
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
