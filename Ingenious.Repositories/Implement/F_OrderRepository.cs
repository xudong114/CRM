using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class F_OrderRepository : EntityFrameworkRepository<F_Order>, IF_OrderRepository
    {
        public F_OrderRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<ComplexOrder> GetAll(ISpecification<F_Order> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;

            var query = from o in context.F_Orders.Where(spec.GetExpression())
                        join store in context.F_Stores on o.StoreCode equals store.Code into co
                        from c in co.DefaultIfEmpty()

                        join clerk in context.F_UserDetails on o.ClerkCode equals clerk.Code into clerkData
                        from cl in clerkData.DefaultIfEmpty()

                        join gojiaju in context.F_UserDetails on o.GoJiajuClerkCode equals gojiaju.Code into gojiajuData
                        from g in gojiajuData.DefaultIfEmpty()

                        join bc in context.F_UserDetails on o.BankClerk equals bc.Code into bankclerkData
                        from bcd in bankclerkData.DefaultIfEmpty()

                        join bcm in context.F_UserDetails on o.BankManager equals bcm.Code into bankclerkmData
                        from bcdm in bankclerkmData.DefaultIfEmpty()

                        join b in context.F_Banks on o.BankCode equals b.Code into bankData
                        from bd in bankData.DefaultIfEmpty()

                        select new ComplexOrder
                        {
                            GoJiajuClerkCode = o.GoJiajuClerkCode,
                            GoJiajuClerkName = g.Name,
                            BankCode = o.BankCode,

                            BankClerk = o.BankClerk,
                            BankClerkName = bcd.Name,

                            BankManager = o.BankManager,
                            BankManagerName = bcdm.Name,

                            Code = o.Code,
                            ClerkName = cl.Name,
                            StoreName = c.Name,
                            FromBank = o.FromBank,
                            BankName = bd.Name,
                            City = o.City,
                            ClerkCode = o.ClerkCode,
                            Community = o.Community,
                            Country = o.Country,
                            CreatedBy = o.CreatedBy,
                            CreatedDate = o.CreatedDate,
                            DownpaymentAmount = o.DownpaymentAmount,
                            Id = o.Id,
                            IDNo = o.IDNo,
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
                            StoreCode = o.StoreCode,
                            TotalAmount = o.TotalAmount,
                            Version = o.Version
                        };
            return query;
        }

        public IQueryable<Infrastructure.KeyValue<string, int>> AssignOrderClerk(Guid websiteId)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            var query = from b in context.F_Users
                        join u in context.F_UserDetails on b.Id equals u.F_UserId
                        join o in context.F_Orders on u.Code equals o.GoJiajuClerkCode into oData
                        where b.WebsiteId == websiteId 
                        && (b.UserType == Infrastructure.Enum.F_UserTypeEnum.CS)
                        && (b.IsActive)
                        select new Infrastructure.KeyValue<string,int>{ Key = u.Code, Value = oData.Count()};
            return query;
        }
    }
}
