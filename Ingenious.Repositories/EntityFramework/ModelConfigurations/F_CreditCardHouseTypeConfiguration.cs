using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class F_CreditCardHouseTypeConfiguration : EntityTypeConfiguration<F_CreditCardHouse>
    {
        public F_CreditCardHouseTypeConfiguration()
        {
            HasKey(model => model.Id);

            Property(model => model.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
