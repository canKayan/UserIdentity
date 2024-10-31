using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserIdentity.Identity;
using UserIdentity.Models;

namespace UserIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> _userManager;
        public RoleAdminController()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDataContext()));
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
        }

        // GET: RoleAdmin
        public ActionResult Index()
        {
            return View(roleManager.Roles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(string name)
        {
            if (ModelState.IsValid)
            {
                var result = roleManager.Create(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(name);
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            var role = roleManager.FindById(id);
            if (role != null)
            {
                var result = roleManager.Delete(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Role not found" });
            }
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {

            var role = roleManager.FindById(id);

            var allUsers = _userManager.Users.ToList(); // Fetches all users into memory
            var members = new List<ApplicationUser>();
            var nonmembers = new List<ApplicationUser>();
            foreach (var user in allUsers)
            {
                var list = _userManager.IsInRole(user.Id,role.Name) ? members : nonmembers;
                list.Add(user);
            }
            return View(new RoleEditModel()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            });
        }

        [HttpPost]
        public ActionResult Edit(RoleUpdateModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] {})
                {
                    result = _userManager.AddToRole(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    result = _userManager.RemoveFromRole(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}