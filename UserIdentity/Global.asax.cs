using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using System.Data.Entity;
using UserIdentity.DAL;

using System.Web.Http;
using UserIdentity.App_Start;

namespace UserIdentity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer(new CreateDatabaseIfNotExists<ShapeContext>());
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProductContext>());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
