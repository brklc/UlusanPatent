using DenemeIdentitiy.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DenemeIdentitiy.Identity
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "TC Kimlik No")]
        public string IdentityNumber { get; set; }

        [Display(Name = "Şirket")]
        public string CompanyName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        //public RegisterEndDate RegisterEndDate { get; set; }

    }
}