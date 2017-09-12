using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class F_NewsTypeConfiguration : EntityTypeConfiguration<F_News>
    {
        public F_NewsTypeConfiguration()
        {
            HasKey(model => model.Id);
            Property(model => model.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.Key)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
