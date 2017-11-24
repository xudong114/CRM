using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class Base_FactoryTypeConfiguration : EntityTypeConfiguration<Base_Factory>
    {
        public Base_FactoryTypeConfiguration()
        {
            HasKey(user => user.Id);
            Property(user => user.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.IDNo).IsRequired().HasMaxLength(20);
        }
    }
}
