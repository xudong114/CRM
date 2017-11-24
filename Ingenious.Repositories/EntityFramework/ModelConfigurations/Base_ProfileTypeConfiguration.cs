using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class Base_ProfileTypeConfiguration : EntityTypeConfiguration<Base_Profile>
    {
        public Base_ProfileTypeConfiguration()
        {
            HasKey(user => user.Id);
            Property(user => user.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.IDNo).IsRequired().HasMaxLength(20);
            Property(model => model.PersonalPhone).IsRequired().HasMaxLength(20);
        }
    }
}
