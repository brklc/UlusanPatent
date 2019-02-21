using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DenemeIdentitiy.IdentityConfig))]

namespace DenemeIdentitiy
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType=DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath= new PathString("/Account/Login")
            });
        }

        public class EmailService : IIdentityMessageService
        {
            public Task SendAsync(IdentityMessage message)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("burakkiliccc@gmail.com", "Uluşan Patent");
                foreach (var dest in message.Destination.Split(';'))
                {
                    if (string.IsNullOrEmpty(dest) == false)
                    {
                        mail.Bcc.Add(dest);
                    }
                }

                mail.IsBodyHtml = true;
                mail.Subject = message.Subject;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Body = message.Body;

                SmtpClient sc = new SmtpClient();
                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                var credential = new NetworkCredential
                {
                    UserName = "burakkiliccc@gmail.com",
                    Password = "5518764qwe"
                };
                sc.Credentials = credential;

                if (mail.Body != null)
                {
                    sc.Send(mail);
                }

                // Plug in your email service here to send an email.
                return Task.FromResult(0);
            }
        }

    }
}
