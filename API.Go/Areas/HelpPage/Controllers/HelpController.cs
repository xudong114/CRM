using System;
using System.Web.Http;
using System.Web.Mvc;
using API.Go.Areas.HelpPage.ModelDescriptions;
using API.Go.Areas.HelpPage.Models;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;

namespace API.Go.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        private string GetDisplayName(Type type)
        {
            var attributes = type.GetCustomAttributes<DisplayNameAttribute>();

            var enumerator = attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is DisplayNameAttribute)
                    return enumerator.Current.DisplayName;
            }
            return string.Empty;
        }
        private string GetDisplayName(PropertyInfo type)
        {
            var attributes = type.GetCustomAttributes<DisplayNameAttribute>();

            var enumerator = attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is DisplayNameAttribute)
                    return enumerator.Current.DisplayName;
            }
            return string.Empty;
        }


        public ActionResult Model()
        {
            var asm = Assembly.Load("Ingenious.DTO");
            Type[] types = asm.GetTypes();
            var list = new List<Models>();
            foreach(var type in types)
            {
                if (type.Name.IndexOf("F_") == 0)
                list.Add(new Models { Name = type.Name, Description = GetDisplayName(type) });
            }

            //var enumasm = Assembly.Load("Ingenious.Infrastructure");
            //Type[] enumtypes = enumasm.GetTypes();
            //foreach (var type in enumtypes)
            //{
            //    if (type.Name.IndexOf("F_") == 0)
            //        list.Add(new Models { Name = type.Name, Description = GetDisplayName(type) });
            //}

            return View(list);
        }

        public ActionResult Details(string name)
        {
            var asm = Assembly.Load("Ingenious.DTO");
            Type[] types = asm.GetTypes();
            var list = new List<Properies>();
            foreach (var type in types)
            {
                if (type.Name == name)
                {
                    PropertyInfo[] pros = type.GetProperties();
                    foreach (var pro in pros)
                    {
                        list.Add(new Properies { Name = pro.Name, Description = GetDisplayName(pro), Type = pro.PropertyType.Name });
                    }
                }
            }

            //var enumasm = Assembly.Load("Ingenious.Infrastructure");
            //Type[] enumtypes = enumasm.GetTypes();
            //foreach (var type in enumtypes)
            //{
            //    if (type.Name == name)
            //    {
            //        PropertyInfo[] pros = type.GetProperties();
            //        foreach (var pro in pros)
            //        {
            //            list.Add(new Properies { Name = pro.Name, Description = GetDisplayName(pro), Type = pro.PropertyType.Name });
            //        }
            //    }
            //}

            return View(list);
        }

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }

    public class Models
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Properies
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; } 
    }

}