using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DenemeIdentitiy.Identity;
using DenemeIdentitiy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DenemeIdentitiy.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {
        private IdentityDataContext db = new IdentityDataContext();

        public ActionResult Index()
        {
            var brands = db.Brands.Include(b => b.ApplicationUser);
            var result = User.Identity.GetUserId();
            var roles = User.IsInRole("Admin");
            return View(brands.Where(i=> i.ApplicationUserId == result || roles).ToList());
        }
     
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = db.Brands.Find(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }


        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "CompanyName");
            return View();
        }

        public ActionResult References()
        {
            var brands = db.Brands.Include(b => b.ApplicationUser);
            var result = User.Identity.GetUserId();
            var roles = User.IsInRole("Admin");
            var durum = brands.Where(s => s.ApplicationUserId == result || roles).Where(s => s.Status != Statuses.BasvuruOnay).ToList();
            return View(durum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,BrandName,TypeName,RegistrationNo,StartDate,EndDate,RegistrationPhoto,ApplicationUserId,Status,LicenseNumber,ProductTypes,LicenseType,ReasonForIssue,Description")] Brands brands,HttpPostedFileBase RegistrationPhoto)
        {
            if (ModelState.IsValid)
            {
                if (RegistrationPhoto != null && RegistrationPhoto.ContentLength > 0)
                {
                    var extensition = Path.GetExtension(RegistrationPhoto.FileName);
                    if (extensition == ".jpg" || extensition == ".png" || extensition == ".pdf")
                    {
                        var filename = Path.GetFileName(RegistrationPhoto.FileName);
                        var path = Path.Combine(Server.MapPath("~/upload/"), filename);
                        RegistrationPhoto.SaveAs(path);
                        brands.RegistrationPhoto = filename;
                    }
                }
                db.Brands.Add(brands);
                db.SaveChanges();
                if (brands.Status == Statuses.BasvuruOnay)
                {
                    string ss = "";
                    ss += "<?xml version='1.0' encoding='UTF-8'?>";
                    ss += "<mainbody>";
                    ss += "<header>";
                    ss += "<company>NETGSM</company>";
                    ss += "<usercode>8503461679</usercode>";
                    ss += "<password>9NQQ72MX</password>";
                    ss += "<startdate></startdate>";
                    ss += "<stopdate></stopdate>";
                    ss += "<type>1:n</type>";
                    ss += "<msgheader>ULUSAN DAN.</msgheader>";
                    ss += "</header>";
                    ss += "<body>";
                    ss += "<msg><![CDATA[Sayın {1} yetkilisi, {2} isimli ürününüzün patent başvurusu onaylanmıştır. Sisteme girerek kontrol edebilirsiniz. ]]></msg>";
                    ss += "<no>{0}</no>";
                    ss += "</body>";
                    ss += "</mainbody>";

                    var a = db.Users.Where(s => s.Id == brands.ApplicationUserId).SingleOrDefault();
                    ss = string.Format(ss, a.PhoneNumber,a.CompanyName, brands.BrandName);

                    ViewBag.Result = XMLPOST("https://api.netgsm.com.tr/sms/send/xml", ss);
                }

                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", brands.ApplicationUserId);
            return View(brands);
        }

       

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id, HttpPostedFileBase RegistrationPhoto)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Brands brands = db.Brands.Find(id);
           
            if (brands == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "CompanyName", brands.ApplicationUserId);
            return View(brands);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,BrandName,TypeName,RegistrationNo,StartDate,EndDate,RegistrationPhoto,ApplicationUserId,Status,LicenseNumber,ProductTypes,LicenseType,ReasonForIssue,Description")] Brands brands, HttpPostedFileBase RegistrationPhoto)
        {
           
            if (ModelState.IsValid)
            {
                if (RegistrationPhoto != null && RegistrationPhoto.ContentLength > 0)
                {
                    var extensition = Path.GetExtension(RegistrationPhoto.FileName);
                    if (extensition == ".jpg" || extensition == ".png" || extensition == ".pdf")
                    {
                        var filename = Path.GetFileName(RegistrationPhoto.FileName);
                        var path = Path.Combine(Server.MapPath("~/upload/"), filename);
                        RegistrationPhoto.SaveAs(path);
                        brands.RegistrationPhoto = filename;
                    }

                }
                db.Entry(brands).State = EntityState.Modified;
                db.SaveChanges();
                if (brands.Status == Statuses.BasvuruOnay)
                {
                    string ss = "";
                    ss += "<?xml version='1.0' encoding='UTF-8'?>";
                    ss += "<mainbody>";
                    ss += "<header>";
                    ss += "<company>NETGSM</company>";
                    ss += "<usercode>8503461679</usercode>";
                    ss += "<password>9NQQ72MX</password>";
                    ss += "<startdate></startdate>";
                    ss += "<stopdate></stopdate>";
                    ss += "<type>1:n</type>";
                    ss += "<msgheader>ULUSAN DAN.</msgheader>";
                    ss += "</header>";
                    ss += "<body>";
                    ss += "<msg><![CDATA[Sayın {1}, {2} isimli ürününüzün patent başvurusu onaylanmıştır. Sisteme girerek kontrol edebilirsiniz. ]]></msg>";
                    ss += "<no>{0}</no>";
                    ss += "</body>";
                    ss += "</mainbody>";

                    var a = db.Users.Where(s => s.Id == brands.ApplicationUserId).SingleOrDefault();
                    ss = string.Format(ss, a.PhoneNumber, a.CompanyName, brands.BrandName);

                    ViewBag.Result = XMLPOST("https://api.netgsm.com.tr/sms/send/xml", ss);
                }

                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", brands.ApplicationUserId);
            return View(brands);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = db.Brands.Find(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Brands brands = db.Brands.Find(id);
            db.Brands.Remove(brands);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private string XMLPOST(string PostAddress, string xmlData)
        {
            try
            {
                WebClient wUpload = new WebClient();
                HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
            catch
            {
                return "-1";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
