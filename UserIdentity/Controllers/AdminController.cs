using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserIdentity.Identity;

namespace UserIdentity.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        public AdminController()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(_userManager.Users);
        }
    }
}