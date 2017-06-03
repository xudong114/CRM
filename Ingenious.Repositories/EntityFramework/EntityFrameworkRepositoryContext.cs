using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ingenious.Repositories.EntityFramework
{
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext ,IDisposable
    {
        ThreadLocal<IngeniousDbContext> localContext = new ThreadLocal<IngeniousDbContext>(() => new IngeniousDbContext());

        public System.Data.Entity.DbContext Context
        {
            get { return localContext.Value; }
        }

        public override void RegisterNew<T>(T obj)
        {
            this.Context.Set<T>().Add(obj);
            this.Committed = false;
        }

        public override void RegisterModified<T>(T obj) 
        {
            this.Context.Set<T>().Attach(obj);
            this.Context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            this.Committed = false;
        }

        public override void RegisterDeleted<T>(T obj) 
        {
            this.Context.Set<T>().Remove(obj);
            this.Committed = false;
        }

        public override void Commit()
        {
            if(!Committed)
            {
                this.Context.SaveChanges();
                this.Committed = true;
            }
        }

        public override void Rollbakc()
        {
            this.Committed = false;
        }

        void IDisposable.Dispose()
        {
            localContext.Dispose();
        }
    }
}
