using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserIdentity.Identity;
using UserIdentity.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using UserIdentity.Services;


namespace UserIdentity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        
        public AccountController()
        {
            
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            _userManager.PasswordValidator = new CustomPasswordValidator()
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireUppercase = true,
                RequireLowercase = true,
            };

            _userManager.UserValidator = new UserValidator<ApplicationUser>(_userManager)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = true
            };
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.Username;
                user.Email = model.EmailAddress;


                var result = _userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    _userManager.AddToRole(user.Id, "User");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Erişim hakkınız yok." });
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Find(model.UserName, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Yanlış kullanıcı adı veya parola");
                }
                else
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identity = _userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperities = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };

                    authManager.SignOut();
                    authManager.SignIn(authProperities, identity);


                    return Redirect(String.IsNullOrEmpty(returnUrl) ? "/Home/Index" : returnUrl);
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login");
        }


        [AllowAnonymous]
        public ActionResult PasswordReset()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult PasswordReset(ResetPasswordViewModel model)
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                //If The User Exists Then Change the Password
                var result = _userManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if(result.Succeeded)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    authManager.SignOut();
                    TempData["Success"] = true;
                    return RedirectToAction("Login");
                    
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }

                    return View(model);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult PasswordResetForgot(string userId)
        {
            ViewBag.userId = userId;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult PasswordResetForgot(ResetPasswordForgotViewModel model, string userId)
        {
            var user = _userManager.FindById(userId);
            if (user != null)
            {
                //If The User Exists Then Change the Password
                var result = (_userManager.RemovePassword(userId)); // Token üretirken hata aldım.
                result = _userManager.AddPassword(userId, model.NewPassword);
                if (result.Succeeded)
                {
                    TempData["Success"] = true;
                    return RedirectToAction("Login");

                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }

                    return View(model);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }




        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usr = _userManager.FindByEmail(model.Email);
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (usr == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordNotConfirmed");
                }
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                //string _code = _userManager.GeneratePasswordResetToken(user.Id);
              //  Console.WriteLine("Reset Token: " + _code);
                var callbackUrl = Url.Action("PasswordResetForgot", "Account", new { userId = user.Id}, protocol: Request.Url.Scheme);
                // await _userManager.SendEmailAsync(user.Id, "PasswordReset", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                bool IsSendEmail = SendEmail.EmailSend(model.Email, "Reset Your Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>", true);
                if (IsSendEmail)
                {
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }

            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        
        public ActionResult ForgotPasswordConfirmation()
        {
            return View(); 
        }


    }
}
// b70147a6-f7e9-4e62-bfca-d48259f6e0c0 user ıd paslıyor
