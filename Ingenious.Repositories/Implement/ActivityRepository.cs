using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Linq;

namespace Ingenious.Repositories.Implement
{
    public class ActivityRepository : EntityFrameworkRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Activity> GetAll(ISpecification<Activity> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Activities.Where(spec.GetExpression());
        }

        public Activity GetByKey(Guid id)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Activities.Where(item => item.Id == id).ToList().FirstOrDefault();
        }
    }
}
