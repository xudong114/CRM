﻿using Ingenious.Domain.Models;
using Ingenious.Repositories.EntityFramework.ModelConfigurations;
using System.Data.Entity;
using System.Reflection;
using System.Linq;
using System.Data.Entity.ModelConfiguration;
using System;

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

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityCategory> ActivityCategories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<PriceStrategy> PriceStrategies { get; set; }
        public DbSet<PriceStrategyDetail> PriceStrategyDetails { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Recharge> Recharges { get; set; }

        #endregion
    }
}
