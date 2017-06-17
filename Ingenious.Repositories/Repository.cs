using Ingenious.Domain;
using System;
using System.Linq;

namespace Ingenious.Repositories
{
    public abstract class Repository<T> : IRepository<T>
        where T : class ,IAggregateRoot
    {
        private readonly IRepositoryContext _Context;

        public Repository(IRepositoryContext context)
        {
            this._Context = context;
        }

        protected abstract void DoAdd(T aggregateRoot);
        protected abstract void DoUpdate(T aggregateRoot);
        protected abstract void DoDelete(T aggregateRoot);
        protected abstract T DoGetByKey(Guid key);

        public void Add(T aggregateRoot)
        {
            this.DoAdd(aggregateRoot);
        }

        public void Update(T aggregateRoot)
        {
            this.DoUpdate(aggregateRoot);
        }

        public void Delete(T aggregateRoot)
        {
            this.DoDelete(aggregateRoot);
        }

        public T GetByKey(Guid id)
        {
            return this.DoGetByKey(id);
        }

        public IRepositoryContext Context
        {
            get { return this._Context; }
        }

        public abstract IQueryable<T> Data { get; }
    }
}
