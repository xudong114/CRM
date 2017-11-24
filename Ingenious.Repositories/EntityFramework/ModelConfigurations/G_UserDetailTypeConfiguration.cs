using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class G_UserDetailTypeConfiguration : EntityTypeConfiguration<G_UserDetail>
    {
        public G_UserDetailTypeConfiguration()
        {
            HasKey(model => model.Id);

            Property(model => model.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.Name)
                .IsRequired();

        }
    }
}
