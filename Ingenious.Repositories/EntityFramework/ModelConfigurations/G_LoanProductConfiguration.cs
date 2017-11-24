using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class G_LoanProductConfiguration : EntityTypeConfiguration<G_LoanProduct>
    {
        public G_LoanProductConfiguration()
        {
            HasKey(user => user.Id);
            Property(user => user.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
