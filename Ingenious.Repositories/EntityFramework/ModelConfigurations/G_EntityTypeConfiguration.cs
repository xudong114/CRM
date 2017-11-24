using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class G_EntityTypeConfiguration : EntityTypeConfiguration<G_Entity>
    {
        public G_EntityTypeConfiguration()
        {
            HasKey(user => user.Id);
            Property(user => user.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.Name)
                .HasMaxLength(20)
                .IsRequired();
        }

    }
}
