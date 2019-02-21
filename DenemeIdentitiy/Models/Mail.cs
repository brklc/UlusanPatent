using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DenemeIdentitiy.Models
{
    public class Mail
    {
        public static void MailSender(string body)
        {
            using (Identity.IdentityDataContext context = new Identity.IdentityDataContext())
            {
                var emails = context.Users.Select(p => p.Email).ToList();

                string emailsConcat = string.Join(";", emails);

                var fromAddress = new MailAddress("burakkiliccc@gmail.com");
                var toAddress = new MailAddress(emailsConcat);
                string subject = "Bilgilendirme";
                using (var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "5518764qwe")
                })
                {
                    using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                    {
                        message.IsBodyHtml = true;
                        smtp.Send(message);
                    }
                }
            }
        }
    }
}