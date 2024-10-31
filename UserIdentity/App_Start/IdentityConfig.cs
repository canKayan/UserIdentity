using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SendGrid.Helpers.Mail;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using UserIdentity.DAL;
using UserIdentity.Identity;


[assembly: OwinStartup(typeof(UserIdentity.IdentityConfig))]

namespace UserIdentity
{
    public class IdentityConfig
    {
        public static IDataProtectionProvider DataProtectionProvider { get; set; }
        public void Configuration(IAppBuilder app)
        {
       
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }


}