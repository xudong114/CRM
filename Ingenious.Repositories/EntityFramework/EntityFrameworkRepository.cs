using Ingenious.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.EntityFramework
{
    public class EntityFrameworkRepository<T> : Repository<T>
        where T: class ,IAggregateRoot
    {
        private readonly IEntityFrameworkRepositoryContext _EFContext;
        protected IEntityFrameworkRepositoryContext EFContext
        {
            get
            {
                return this._EFContext;
            }
        }

        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
            if(context is IEntityFrameworkRepositoryContext)
            {
                this._EFContext = context as IEntityFrameworkRepositoryContext;
            }
        }

        protected override void DoAdd(T aggregateRoot)
        {
            this._EFContext.RegisterNew<T>(aggregateRoot);
        }

        protected override void DoUpdate(T aggregateRoot)
        {
            this._EFContext.RegisterModified<T>(aggregateRoot);
        }

        protected override void DoDelete(T aggregateRoot)
        {
            this._EFContext.RegisterDeleted<T>(aggregateRoot);
        }

        protected override T DoGetByKey(Guid key)
        {
            return this._EFContext.Context.Set<T>().Find(key);
        }
    }
}
