using Ingenious.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            var isInit = bool.Parse(ConfigurationManager.AppSettings["IsInit"].ToString());
            this.Init(context, isInit);

            string indexFormat = "IF NOT EXISTS( SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('{0}', N'U') and NAME='{1}') ";

            //佳居贷，银行编码唯一性索引。
            context.Database.ExecuteSqlCommand(string.Format(indexFormat, "G_Bank", "IX_G_Bank_Code") + " CREATE UNIQUE INDEX IX_G_Bank_Code ON G_Bank(Code)");
            //GO佳居基础数据：个人信息，身份证编码唯一性索引。
            context.Database.ExecuteSqlCommand(string.Format(indexFormat, "Base_Profile", "IX_Base_Profile_IDNO") + " CREATE UNIQUE INDEX IX_Base_Profile_IDNO ON Base_Profile(IDNO)");
            //GO佳居基础数据：个人信息，手机号码唯一性索引。
            context.Database.ExecuteSqlCommand(string.Format(indexFormat, "Base_Profile", "IX_Base_Profile_PersonalPhone") + " CREATE UNIQUE INDEX IX_Base_Profile_PersonalPhone ON Base_Profile(PersonalPhone)");

            base.Seed(context);
        }

        private void Init(IngeniousDbContext context,bool isExcute)
        {
            if (!isExcute)
                return;
            var dept = new Department
            {
                Id = Guid.NewGuid(),
                Name = "全公司",
                ParentId = null,
                IsActive = true,
                Version = 1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                ModifiedBy = Guid.NewGuid()
            };
            context.Departments.Add(dept);

            var account = new Account
            {
                Id = new Guid(),
                DepartmentId = dept.Id,
                Balance = 0.0m,
                Version=1,
                IsActive = true,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                ModifiedBy = Guid.NewGuid()
            };
            context.Accounts.Add(account);

            var userDetail = new UserDetail
            {
                Id = Guid.NewGuid(),
                Name = "管理员",
                Logo = Infrastructure.GlobalMessage.DefaultFace,
                IsActive=true,
                Version = 1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                ModifiedBy = Guid.NewGuid()
            };

            var user = new User
            {
                Id = new Guid(),
                UserName = "admin",
                BranchId = dept.Id,
                IsActive = true,
                IsAdmin = true,
                IsSupper = true,
                Password = "c8837b23ff8aaa8a2dde915473ce0991",
                Status = Infrastructure.Enum.UserStatusEnum.Available,
                UserDetail = userDetail,
                Version = 1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                ModifiedBy = Guid.NewGuid()
            };

            context.Users.Add(user);

            var dataItemList = new List<Dictionary> { 
                 new Dictionary
                 {
                    Id=new Guid(),Category = "客户级别",Name ="A", Code="A", Description ="重点客户",IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(),Category = "客户级别",Name ="B",Code="B",  Description ="普通客户",IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(),Category = "客户级别",Name ="C", Code="C", Description ="非优先客户",IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(),Category = "客户活动类型",Name ="电话",Code="电话", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "客户活动类型", Name ="快速记录",Code="快速记录", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "客户活动类型", Name ="拜访签到", Code="拜访签到", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                 new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="金融", Code="金融", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="电信", Code="电信", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="教育", Code="教育", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="高科技", Code="高科技", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="政府", Code="政府", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="制造业", Code="制造业", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="服务", Code="服务", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="能源", Code="能源", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="零售", Code="零售", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="媒体", Code="媒体", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="娱乐", Code="娱乐", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="咨询", Code="咨询", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="非营利事业", Code="非营利事业", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="公用事业", Code="公用事业", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                },
                new Dictionary
                 {
                    Id=new Guid(), Category = "行业", Name ="其它", Code="其它", IsActive=true,Sort=0,Version=1,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = user.Id,ModifiedBy = user.Id
                }
            };

            context.Dictionaries.AddRange(dataItemList);

        }
    }
}
