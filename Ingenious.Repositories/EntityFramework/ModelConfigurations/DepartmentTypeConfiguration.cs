using Ingenious.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ingenious.Repositories.EntityFramework.ModelConfigurations
{
    public class DepartmentTypeConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentTypeConfiguration()
        {
            HasKey(model => model.Id);

            Property(model => model.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(model => model.Name)
                .IsRequired();

            //HasOptional<Department>(model => model.Parent)
            //    .WithRequired()
            //    .WillCascadeOnDelete(false);

            //HasOptional<PriceStrategy>(model => model.PriceStrategy)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(false);

            /*
             *  public Guid? ParentId { get; set; }
             *  public virtual Department Parent { get; set; }
             *  
             *  public virtual ICollection<Department> Children { get; set; }
             *  
             *  Children的数据获取是可以根据上面的ParentId 和 Parent 自动推断出来
             *  可以不需要如下的配置。
             *  
             */
            //HasMany<Department>(model => model.Children)
            //    .WithOptional(model => model.Parent)
            //    .HasForeignKey(model => model.ParentId);

            
        }
    }
}
