using DenemeIdentitiy.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DenemeIdentitiy.Models
{
    public class RegistrationDocument
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Organik Gübre")]
        [DataType(DataType.Upload)]
        public string Compost { get; set; }

        [Display(Name = "Kimyevi Gübre")]
        [DataType(DataType.Upload)]
        public string Fertilizer { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}