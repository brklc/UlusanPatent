using DenemeIdentitiy.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DenemeIdentitiy.Controllers
{
    
    public class HomeController : Controller
    {
        private IdentityDataContext db = new IdentityDataContext();

        [Authorize]
        public ActionResult Index()
        {
            var result = User.Identity.GetUserId();
            var brandsCountUser = db.Brands.Where(i => i.ApplicationUserId == result).Count();
            ViewBag.CountUser = brandsCountUser;

            var registration = db.RegistrationDocuments.Where(i => i.ApplicationUserId == result).Count();
            ViewBag.RegistrationCount = registration;

            var brands = db.Brands.Count();
            ViewBag.Count = brands;

            var users = db.Users.Count();
            ViewBag.UsersCount = users;

            var registrations = db.RegistrationDocuments.Count();
            ViewBag.Registrations = registrations;

            return View();
        }

        public ActionResult LoginError()
        {
            return View();
        }
    }
}