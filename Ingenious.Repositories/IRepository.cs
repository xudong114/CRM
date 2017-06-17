using Ingenious.Domain;
using Ingenious.Infrastructure;
using System;
using System.Linq;

namespace Ingenious.Repositories
{
    public interface IRepository<T>
        where T : class, IAggregateRoot
    {
        IRepositoryContext Context { get; }
        void Add(T aggregateRoot);
        void Update(T aggregateRoot);
        void Delete(T aggregateRoot);
        T GetByKey(Guid id);

        IQueryable<T> Data { get; }
    }
}
