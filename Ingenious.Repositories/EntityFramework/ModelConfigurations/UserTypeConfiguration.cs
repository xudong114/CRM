using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class UserTypeConfiguration : EntityTypeConfiguration<User>
    {
        public UserTypeConfiguration()
        {
            HasKey(user => user.Id);
            Property(user => user.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasOptional<Department>(model => model.Department);
            //HasOptional<Department>(model => model.Dept);
            //HasRequired<UserDetail>(model => model.UserDetail)
            //    .WithOptional()
            //    .WillCascadeOnDelete();
           
        }
    }
}
