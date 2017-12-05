using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ingenious.Repositories
{
    public abstract class RepositoryContext : IRepositoryContext
    {
        private readonly Guid _id = Guid.NewGuid();


        private readonly ThreadLocal<bool> _localCommitted = new ThreadLocal<bool>(() => true);
        private readonly ThreadLocal<Dictionary<Guid, object>> _localNewList = new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());
        private readonly ThreadLocal<Dictionary<Guid, object>> _localModifiedList = new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());
        private readonly ThreadLocal<Dictionary<Guid, object>> _localDeletedList = new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());


        #region IRepositoryContext
        public Guid Id
        {
            get { return this._id; }
        }

        public virtual void RegisterNew<T>(T obj) where T : class, Domain.IAggregateRoot
        {
            if (obj.Id.Equals(Guid.Empty))
                throw new ArgumentNullException("对象标识列为空。", "obj");
            if (_localModifiedList.Value.ContainsKey(obj.Id))
                throw new InvalidOperationException("编辑状态的对象无法被标记为创建状态。");
            if(_localNewList.Value.ContainsKey(obj.Id))
                throw new InvalidOperationException("对象已经被标记为创建状态。");

            _localNewList.Value.Add(obj.Id, obj);
            _localCommitted.Value = false;

        }

        public virtual void RegisterModified<T>(T obj) where T : class, Domain.IAggregateRoot
        {
            if (obj.Id.Equals(Guid.Empty))
                throw new ArgumentNullException("对象标识列为空。", "obj");
            if (_localDeletedList.Value.ContainsKey(obj.Id))
                throw new InvalidOperationException("删除状态的对象无法标记为编辑状态。");
            if (!_localModifiedList.Value.ContainsKey(obj.Id)
                && !_localNewList.Value.ContainsKey(obj.Id))
                _localModifiedList.Value.Add(obj.Id, obj);
            _localCommitted.Value = false;
        }

        public virtual void RegisterDeleted<T>(T obj) where T : class, Domain.IAggregateRoot
        {
            if (obj.Id.Equals(Guid.Empty))
                throw new ArgumentNullException("对象标识列为空。", "obj");
            if(_localNewList.Value.ContainsKey(obj.Id))
            {
                if (_localNewList.Value.Remove(obj.Id))
                    return;
            }
            bool removedStatus = _localModifiedList.Value.Remove(obj.Id);
            bool addedStatus = false;

            if(!_localDeletedList.Value.ContainsKey(obj.Id))
            {
                _localDeletedList.Value.ContainsKey(obj.Id);
                addedStatus = true;
            }

            _localCommitted.Value = !(removedStatus || addedStatus);
        }

        #endregion

        #region UnitOfWork
        public bool Committed
        {
            get { return _localCommitted.Value; }
            protected set
            {
                _localCommitted.Value = value;
            }
        }

        public abstract void Commit();

        public abstract void Rollbakc();

        #endregion


        public void Dispose()
        {
            _localCommitted.Dispose();
            _localDeletedList.Dispose();
            _localModifiedList.Dispose();
            _localNewList.Dispose();
        }
    }
}
