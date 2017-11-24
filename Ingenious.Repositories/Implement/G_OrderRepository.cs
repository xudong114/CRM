using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Infrastructure;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class G_OrderRepository : EntityFrameworkRepository<G_Order>, IG_OrderRepository
    {
        public G_OrderRepository(IRepositoryContext context)
            : base(context)
        {

        }
        /// <summary>
        /// 查询订单数据
        /// </summary>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="spec">过滤条件</param>
        /// <param name="sort">排序条件</param>
        /// <returns></returns>
        public PagedResult<G_ComplexOrder> GetAll(int pageIndex, int pageSize, ISpecification<G_Order> spec, string sort = "")
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            var query = from o in context.G_Orders.Where(spec.GetExpression())
                        //join store in context.Base_Stores on o.StoreCode equals store.Code into co
                        //from c in co.DefaultIfEmpty()

                        //join clerk in context.G_UserDetails on o.ClerkCode equals clerk.Code into clerkData
                        //from cl in clerkData.DefaultIfEmpty()

                        join gojiaju in context.G_UserDetails on o.GoJiajuClerkCode equals gojiaju.Code into gojiajuData
                        from g in gojiajuData.DefaultIfEmpty()

                        join gojiajumanager in context.G_UserDetails on o.GoJiajuClerkCode equals gojiajumanager.Code into gojiajumanagerData
                        from gm in gojiajumanagerData.DefaultIfEmpty()

                        join bc in context.G_UserDetails on o.BankClerk equals bc.Code into bankclerkData
                        from bcd in bankclerkData.DefaultIfEmpty()

                        join bcm in context.G_UserDetails on o.BankManager equals bcm.Code into bankclerkmData
                        from bcdm in bankclerkmData.DefaultIfEmpty()

                        join b in context.G_Banks on o.BankCode equals b.Code into bankData
                        from bd in bankData.DefaultIfEmpty()

                        join p in context.G_LoanProducts on o.ProductCode equals p.Code into productData
                        from pd in productData.DefaultIfEmpty()

                        select new G_ComplexOrder
                        {
                            //GO佳居金融客服
                            GoJiajuClerkCode = o.GoJiajuClerkCode,
                            GoJiajuClerkName = g.Name,
                            GoJiajuClerkOfficePhone = g.G_User.UserName,
                            //GO佳居金融经理
                            GoJiajuManagerCode = o.GoJiajuManagerCode,
                            GoJiajuManagerName = gm.Name,
                            GoJiajuManagerOfficePhone = gm.G_User.UserName,

                            BankCode = o.BankCode,

                            BankClerk = o.BankClerk,
                            BankClerkName = bcd.Name,

                            BankManager = o.BankManager,
                            BankManagerName = bcdm.Name,

                            Code = o.Code,
                            //ClerkName = cl.Name,
                            //StoreName = c.Name,
                            FromBank = o.FromBank,
                            BankName = bd.Name,
                            City = o.City,
                            //ClerkCode = o.ClerkCode,
                            Community = o.Community,
                            Country = o.Country,
                            CreatedBy = o.CreatedBy,
                            CreatedDate = o.CreatedDate,
                            //DownpaymentAmount = o.DownpaymentAmount,
                            Id = o.Id,
                            IDNo = o.IDNo,
                            IDImg = o.IDImg,
                            IsActive = o.IsActive,
                            IsInstallment = o.IsInstallment,
                            Landlord = o.Landlord,
                            LoanAmount = o.LoanAmount,
                            GotLoanAmount = o.GotLoanAmount,
                            ModifiedBy = o.ModifiedBy,
                            ModifiedDate = o.ModifiedDate,
                            Name = o.Name,
                            PersonalPhone = o.PersonalPhone,
                            Province = o.Province,
                            Status = o.Status,
                            //StoreCode = o.StoreCode,
                            //TotalAmount = o.TotalAmount,
                            Version = o.Version,
                            StoreIds = o.StoreIds,
                            Stores = (from s in context.Base_Stores where s.IDNo == o.IDNo select s).ToList<Base_Store>(),
                            Factory = (from f in context.Base_Factorys where f.IDNo == o.IDNo select f).FirstOrDefault(),
                            ProductCode = pd.Code,
                            ProductName = pd.Name
                        };

            switch (sort)
            {
                case "createddate_desc":
                    {
                        query = query.OrderByDescending(item => item.CreatedDate);
                    }
                    break;
                case "createddate":
                    {
                        query = query.OrderBy(item => item.CreatedDate);
                    }
                    break;
                default:
                    {
                        query = query.OrderByDescending(item => item.CreatedDate);
                    }
                    break;
            }

            int skip;
            try
            {
                skip = checked((pageIndex - 1) * pageSize);
            }
            catch (OverflowException)
            {
                skip = 0;
            }

            int take = pageSize;
            var pagedQuery = query.Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
            if (pagedQuery == null)
                return new PagedResult<G_ComplexOrder>();
            return new PagedResult<G_ComplexOrder>(pagedQuery.Key.Total, (pagedQuery.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedQuery.Select(p => p).ToList());

        }

        /// <summary>
        /// 获取订单所有状态的数量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public ComplexStatusCount GetComplexStatusCount(ISpecification<G_Order> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            var query = context.G_Orders.Where(spec.GetExpression());

            var model = new ComplexStatusCount();

            model.BankDeniedCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.BankDenied).Count();
            model.BankPassedCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.BankPassed).Count();
            model.BankSignedCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.BankSigned).Count();
            model.CanceledCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.Canceled).Count();
            model.GojiajuDeniedCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.GojiajuDenied).Count();
            model.GojiajuPassedCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.GojiajuPassed).Count();
            model.InProcessCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.InProcess).Count();
            model.SignCanceledCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.SignCanceled).Count();
            model.SuccessedCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.Successed).Count();
            model.TempCount = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.Temp).Count();
            model.PreProcess = query.Where(item => item.Status == Infrastructure.Enum.G_OrderStatusEnum.PreProcess).Count();
            return model;
        }

        /// <summary>
        /// 获取订单状态的数量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public int GetStatusCount(ISpecification<G_Order> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            var query = context.G_Orders.Where(spec.GetExpression());

            return query.Count();
        }

        public IQueryable<Infrastructure.KeyValue<string, int>> AssignOrderClerk(Guid websiteId)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from b in context.F_Users
                        join u in context.F_UserDetails on b.Id equals u.F_UserId
                        join o in context.G_Orders on u.Code equals o.GoJiajuClerkCode into oData
                        where b.WebsiteId == websiteId
                        && (b.UserType == Infrastructure.Enum.F_UserTypeEnum.CS)
                        && (b.IsActive)
                        select new Infrastructure.KeyValue<string, int> { Key = u.Code, Value = oData.Count() };
            return query;
        }

    }
}
