using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class ClientTypeConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientTypeConfiguration()
        {
            HasKey(model => model.Id);

            Property(model => model.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.Name)
                .IsRequired();

            //HasRequired<Department>(model => model.Department)
            //    .WithOptional()
            //    .WillCascadeOnDelete(false);

            //HasOptional<User>(model => model.User)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(false);
            //HasOptional<Industry>(model => model.Industry)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(false);
            //HasOptional<Grade>(model => model.Grade)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(false);
        }
    }
}
