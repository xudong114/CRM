﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ingenious.Infrastructure.IoC
{
    /// <summary>
    /// Ref Unity.Mvc5
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        private const string HttpContextKey = "perRequestContainer";

        private readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (typeof(IController).IsAssignableFrom(serviceType))
            {
                return ChildContainer.Resolve(serviceType);
            }

            return IsRegistered(serviceType) ? ChildContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (IsRegistered(serviceType))
            {
                yield return ChildContainer.Resolve(serviceType);
            }

            foreach (var service in ChildContainer.ResolveAll(serviceType))
            {
                yield return service;
            }
        }

        protected IUnityContainer ChildContainer
        {
            get
            {
                var childContainer = System.Web.HttpContext.Current.Items[HttpContextKey] as IUnityContainer;

                if (childContainer == null)
                {
                    System.Web.HttpContext.Current.Items[HttpContextKey] = childContainer = _container.CreateChildContainer();
                }

                return childContainer;
            }
        }

        public static void DisposeOfChildContainer()
        {
            var childContainer = System.Web.HttpContext.Current.Items[HttpContextKey] as IUnityContainer;

            if (childContainer != null)
            {
                childContainer.Dispose();
            }
        }

        private bool IsRegistered(Type typeToCheck)
        {
            var isRegistered = true;

            if (typeToCheck.IsInterface || typeToCheck.IsAbstract)
            {
                isRegistered = ChildContainer.IsRegistered(typeToCheck);

                if (!isRegistered && typeToCheck.IsGenericType)
                {
                    var openGenericType = typeToCheck.GetGenericTypeDefinition();

                    isRegistered = ChildContainer.IsRegistered(openGenericType);
                }
            }

            return isRegistered;
        }
    }
}
