using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DenemeIdentitiy.Models
{
    public class MailModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Mesaj Başlığı")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Mesaj")]
        public string Message { get; set; }

        public DateTime CreateDate { get; set; }
    }
}