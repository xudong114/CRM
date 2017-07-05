using Ingenious.Infrastructure.IoC;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
//using System.Web.Mvc;

namespace API.Go.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            Ingenious.Application.ApplicationService.Initialize();
            Ingenious.Application.DependencyRegisterType.Register(ref container);
            config.DependencyResolver = new UnityDependencyResolverAPI(container);
        }
    }
}