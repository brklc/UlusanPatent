using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DenemeIdentitiy.Identity
{
    public class IdentityDataContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext():base("identityConnection")
        {
             
        }

        public System.Data.Entity.DbSet<DenemeIdentitiy.Models.Brands> Brands { get; set; }
        public System.Data.Entity.DbSet<DenemeIdentitiy.Models.RegistrationDocument> RegistrationDocuments { get; set; }
        public System.Data.Entity.DbSet<DenemeIdentitiy.Models.MailModel> Mails { get; set; }

    }
}