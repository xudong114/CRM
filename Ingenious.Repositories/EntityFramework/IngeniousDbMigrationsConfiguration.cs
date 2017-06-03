using Ingenious.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace Ingenious.Repositories.EntityFramework
{
    public class IngeniousDbMigrationsConfiguration : DbMigrationsConfiguration<IngeniousDbContext>
    {
        public IngeniousDbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(IngeniousDbContext context)
        {
            //var userDetailId = Guid.NewGuid();
            //context.UserDetails.Add(new UserDetail
            //{
            //    Id = userDetailId,
            //    Name = "赵佳宏",
            //    CreatedDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now,
            //    CreatedBy = Guid.NewGuid(),
            //    ModifiedBy = Guid.NewGuid()
            //});
            //context.Users.Add(new User { 
            // UserName = "admin",
             
            //});

            //context.Departments.RemoveRange(context.Departments);
            //context.Departments.AddRange(new List<Department> { 
            //    new Department{ 
            //        Id=Guid.NewGuid(),
            //        Name="全公司",
            //        ParentId = null,
            //        CreatedDate=DateTime.Now,
            //        ModifiedDate=DateTime.Now,
            //        CreatedBy = Guid.NewGuid(),
            //        ModifiedBy= Guid.NewGuid()
            //     }
            //});

            base.Seed(context);
        }
    }
}
