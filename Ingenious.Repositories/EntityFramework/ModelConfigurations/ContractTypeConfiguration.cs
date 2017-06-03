using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class ContractTypeConfiguration : EntityTypeConfiguration<Contract>
    {
        public ContractTypeConfiguration()
        {
            HasKey(model => model.Id);

            Property(model => model.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.Title)
                .IsRequired();
            //HasRequired<Department>(model => model.Department)
            //    .WithOptional()
            //    .WillCascadeOnDelete(false);
            //HasRequired<User>(model => model.Owner)
            //    .WithOptional()
            //    .WillCascadeOnDelete(false);
            //HasRequired<Client>(model => model.Client)
            //    .WithOptional()
            //    .WillCascadeOnDelete(false);
            //HasRequired<Product>(model => model.Product)
            //    .WithOptional()
            //    .WillCascadeOnDelete(false);
            //HasOptional<User>(model => model.Principal);
        }
    }
}
