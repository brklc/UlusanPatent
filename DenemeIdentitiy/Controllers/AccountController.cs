using DenemeIdentitiy.Identity;
using DenemeIdentitiy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DenemeIdentitiy.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IdentityDataContext db = new IdentityDataContext();

        public AccountController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            userManager.PasswordValidator = new CustomPasswordValidator()
            {
                 RequireDigit=true,
                 RequiredLength=7,
                 RequireLowercase=false,
                 RequireUppercase =false,
                 RequireNonLetterOrDigit=false
            };
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = false
            };
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
           
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var result = db.Users.ToList();
            return View(result);
        }

        public ActionResult Edit(string id)
        {
            var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
            return View(user);
        }


        [HttpPost]
        public ActionResult Edit(ApplicationUser users)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(u => u.Id == users.Id).FirstOrDefault();
                user.UserName = users.UserName;
                user.Email = users.Email;
                user.PhoneNumber = users.PhoneNumber;
                user.IdentityNumber = users.IdentityNumber;
                user.CompanyName = users.CompanyName;
                user.EndDate = user.EndDate;
                db.SaveChanges();

            }
            return RedirectToAction("UserList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.IdentityNumber = model.IdentityNumber;
                user.CompanyName = model.CompanyName;
                user.StartDate = DateTime.Now;
                var red = (int)model.RegisterEndDate;
                if(red == 1)
                {
                    user.EndDate= user.StartDate.Date.AddMonths(6);
                }
                else if(red == 2)
                {
                    user.EndDate = user.StartDate.Date.AddYears(1);
                }
                var result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                    return RedirectToAction("Login");
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
                return View("Error", new string[] { "Erişim Hakkınız Yok!" });
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
            var user = userManager.Find(model.UserName, model.Password);
            
            if(user == null)
            {
                ModelState.AddModelError("","Yanlış kullanıcı adı veya parola");
            }
            else if(!(user.EndDate < DateTime.Now))
            {
                var authManager = HttpContext.GetOwinContext().Authentication;

                var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties
                {
                        IsPersistent = model.Remeberme
                };
                authManager.SignOut();
                authManager.SignIn(authProperties,identity);

                return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
            }
                else
                {
                    return RedirectToAction("LoginError","Home");
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }


        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Login");
        }
    }
}