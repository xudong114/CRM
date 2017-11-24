using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class Base_CarTypeConfiguration : EntityTypeConfiguration<Base_Car>
    {
        public Base_CarTypeConfiguration()
        {
            HasKey(user => user.Id);
            Property(user => user.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.IDNo).IsRequired().HasMaxLength(20);
        }
    }
}
