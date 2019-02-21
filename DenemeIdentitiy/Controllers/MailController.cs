using DenemeIdentitiy.Identity;
using DenemeIdentitiy.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static DenemeIdentitiy.IdentityConfig;

namespace DenemeIdentitiy.Controllers
{
    [Authorize(Roles ="Admin")]
    public class MailController : Controller
    {
        private IdentityDataContext db = new IdentityDataContext();

        public ActionResult Index()
        {
            var model = db.Mails.OrderByDescending(a => a.CreateDate).ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(MailModel mails)
        {
            if (ModelState.IsValid)
            {
                using (IdentityDataContext context = new IdentityDataContext())
                {
                    MailModel entity = new MailModel
                    {
                        Id = mails.Id,
                        Title = mails.Title,
                        Message = mails.Message,
                        CreateDate = DateTime.Now
                    };

                    var emails = context.Users.Select(p => p.Email).ToList();
                    string emailsConcat = string.Join(";", emails);
                    EmailService emailService = new EmailService();

                    var body = new StringBuilder();
                    body.AppendLine(mails.Message);

                    if(mails.Title != null && mails.Message != null)
                    {
                        emailService.SendAsync(new IdentityMessage()
                        {
                            Subject = mails.Title,
                            Body = body.ToString(),
                            Destination = emailsConcat
                        }

                      );
                        context.Mails.Add(entity);
                        context.SaveChanges();
                        TempData["success"] = "Mail Başarıyla Gönderilmiştir.";
                    }
                }
            }
            return View();
        }
    }
}