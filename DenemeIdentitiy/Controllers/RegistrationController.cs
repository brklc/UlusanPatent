using DenemeIdentitiy.Identity;
using DenemeIdentitiy.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DenemeIdentitiy.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        private IdentityDataContext db = new IdentityDataContext();

        public ActionResult Index()
        {
            var brands = db.RegistrationDocuments.Include(b => b.ApplicationUser);
            var result = User.Identity.GetUserId();
            var roles = User.IsInRole("Admin");
            return View(brands.Where(i => i.ApplicationUserId == result || roles).ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "CompanyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Compost,Fertilizer,ApplicationUserId")] RegistrationDocument registrationDocument, HttpPostedFileBase Compost,HttpPostedFileBase Fertilizer)
        {
            if (ModelState.IsValid)
            {
                if (Compost != null && Compost.ContentLength > 0)
                {
                    string extensition = Path.GetExtension(Compost.FileName);
                   
                    if (extensition == ".jpg" || extensition == ".png" || extensition == ".pdf")
                    {
                        var filename = Path.GetFileName(Compost.FileName);
                        var path = Path.Combine(Server.MapPath("~/upload/"), filename);
                        Compost.SaveAs(path);
                        registrationDocument.Compost = filename;
                    }
                }

                if (Fertilizer != null && Fertilizer.ContentLength > 0)
                {
                    string extensition2 = Path.GetExtension(Fertilizer.FileName);

                    if (extensition2 == ".jpg" || extensition2 == ".png" || extensition2 == ".pdf")
                    {
                        var filename2 = Path.GetFileName(Fertilizer.FileName);
                        var path2 = Path.Combine(Server.MapPath("~/upload/"), filename2);
                        Compost.SaveAs(path2);
                        registrationDocument.Fertilizer = filename2;
                    }
                }
                ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", registrationDocument.ApplicationUserId);
                db.RegistrationDocuments.Add(registrationDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            return View(registrationDocument);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrationDocument registrationDocument = db.RegistrationDocuments.Find(id);
            if (registrationDocument == null)
            {
                return HttpNotFound();
            }
            return View(registrationDocument);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            RegistrationDocument registrationDocument = db.RegistrationDocuments.Find(id);
            db.RegistrationDocuments.Remove(registrationDocument);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}