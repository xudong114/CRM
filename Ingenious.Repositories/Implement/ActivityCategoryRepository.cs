using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class ActivityCategoryRepository : EntityFrameworkRepository<ActivityCategory>, IActivityCategoryRepository
    {
        public ActivityCategoryRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<ActivityCategory> GetAll()
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.ActivityCategories;
        }

    }
}
