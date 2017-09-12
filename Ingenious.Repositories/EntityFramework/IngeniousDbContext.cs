using Ingenious.Domain.Models;
using Ingenious.Repositories.EntityFramework.ModelConfigurations;
using System.Data.Entity;
using System.Reflection;
using System.Linq;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Ingenious.Repositories.EntityFramework
{
    public sealed class IngeniousDbContext : DbContext
    {
        public IngeniousDbContext()
            : base("defaultConnection")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.UseDatabaseNullSemantics = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Configurations
            //    .Add(new UserTypeConfiguration())
            //    .Add(new UserDetailTypeConfiguration())
            //    .Add(new DepartmentTypeConfiguration())
            //    .Add(new ClientTypeConfiguration());

            var entityTypeConfigurations = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                    );

            foreach (var type in entityTypeConfigurations)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        #region Models

        #region 信用卡
        
        public DbSet<F_CreditCardApplication> F_CreditCardApplications { get; set; }
        public DbSet<F_CreditCard> F_CreditCards { get; set; }
        public DbSet<F_CreditCardCar> F_CreditCardCars { get; set; }
        public DbSet<F_CreditCardHouse> F_CreditCardHouses { get; set; }
        public DbSet<F_CreditCardStore> F_CreditCardStores { get; set; }
        public DbSet<F_CreditCardFile> F_CreditCardFiles { get; set; }
        
        #endregion


        #region Api.Go

        public DbSet<F_Store> F_Stores { get; set; }
        public DbSet<F_User> F_Users { get; set; }
        public DbSet<F_UserDetail> F_UserDetails { get; set; }
        public DbSet<F_Bank> F_Banks { get; set; }

        public DbSet<F_Order> F_Orders { get; set; }

        public DbSet<F_OrderRecord> F_OrderRecords { get; set; }

        public DbSet<F_File> F_Files { get; set; }

        public DbSet<F_StoreClerk> F_StoreClerks { get; set; }
        public DbSet<F_Account> F_Accounts { get; set; }
        public DbSet<F_WithdrawDepositRecord> F_WithdrawDepositRecords { get; set; }

        public DbSet<F_Activity> F_Activities { get; set; }
        public DbSet<F_AD> F_ADs { get; set; }
        public DbSet<F_News> F_News { get; set; }
        public DbSet<F_BankOption> F_BankOptions { get; set; }
        #endregion

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<PriceStrategy> PriceStrategies { get; set; }
        public DbSet<PriceStrategyDetail> PriceStrategyDetails { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Recharge> Recharges { get; set; }

        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }
        #endregion
    }
}
