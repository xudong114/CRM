using System;
using System.Web.Http;
using System.Web.Mvc;

namespace Ingenious.Infrastructure.Cache
{
    public sealed class APICacheService : IAPICacheProvider
    {
        #region Private Fields
        private readonly IAPICacheProvider _cacheProvider;
        private static readonly APICacheService instance = new APICacheService();
        #endregion

        #region Ctor
        static APICacheService() { }

        private APICacheService()
        {
            //_cacheProvider = ServiceLocator.Instance.GetService<ICacheProvider>();
            //_cacheProvider = DependencyResolver.Current.GetService<ICacheProvider>();
            _cacheProvider = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAPICacheProvider)) as IAPICacheProvider; 

        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 获取<c>CacheManager</c>类型的单件（Singleton）实例。
        /// </summary>
        public static APICacheService Instance
        {
            get { return instance; }
        }
        #endregion

        #region ICacheProvider Members
        /// <summary>
        /// 向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Add(string key, string valKey, object value)
        {
            _cacheProvider.Add(key, valKey, value);
        }

        public void Add(string key, string valKey, object value, DateTimeOffset datetimeoffset)
        {
            _cacheProvider.Add(key, valKey, value, datetimeoffset);
        }
        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Put(string key, string valKey, object value)
        {
            _cacheProvider.Put(key, valKey, value);
        }
        /// <summary>
        /// 从缓存中读取对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <returns>被缓存的对象。</returns>
        public object Get(string key, string valKey)
        {
            return _cacheProvider.Get(key, valKey);
        }
        /// <summary>
        /// 从缓存中移除对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        public void Remove(string key)
        {
            _cacheProvider.Remove(key);
        }
        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示拥有指定键值的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key)
        {
            return _cacheProvider.Exists(key);
        }
        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示拥有指定键值和缓存值键的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <param name="valKey">缓存值键。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key, string valKey)
        {
            return _cacheProvider.Exists(key, valKey);
        }
        #endregion
    }
}
