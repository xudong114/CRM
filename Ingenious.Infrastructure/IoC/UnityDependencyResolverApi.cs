using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;


namespace Ingenious.Infrastructure.IoC
{
    /// <summary>
    /// Ref Unity.WebAPI
    /// </summary>
    public class UnityDependencyResolverAPI : UnityDependencyScope, IDependencyResolver
    {
        public UnityDependencyResolverAPI(IUnityContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            var childContainer = Container.CreateChildContainer();

            return new UnityDependencyScope(childContainer);
        }
    }    
}
