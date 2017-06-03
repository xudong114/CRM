using Ingenious.Infrastructure.IoC;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            Ingenious.Application.ApplicationService.Initialize();
            Ingenious.Application.DependencyRegisterType.Register(ref container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}