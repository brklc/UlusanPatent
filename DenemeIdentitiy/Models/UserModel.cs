using DenemeIdentitiy.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DenemeIdentitiy.Models
{
    public class Register
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Geçerli E-Mail Formatı Giriniz!")]
        [Display(Name = "E-Mail Adres")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şirket Adı")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(11,MinimumLength =11,ErrorMessage ="TC Kimlik No 11 Hane Olmalıdır!")]
        [Display(Name = "TC Kimlik No")]
        public string IdentityNumber { get; set; }

        [Required]
        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Telefon numarası 0 ile başlayıp, toplam 11 hane olmalıdır!")]
        [Display(Name = "Telefon No:")]
        public string PhoneNumber { get; set; }

        public string TaxNo { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Üyelik Tipi Seçiniz")]
        public RegisterEndDate RegisterEndDate { get; set; }

        public IEnumerable<Brands> Brands { get; set; }
    }

    public class LoginModel
    {
        public LoginModel()
        {
            Remeberme = false;
        }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
     
        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool Remeberme { get; set; }

        public DateTime EndDate { get; set; }
    }
   
   
    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }

    }
    public enum RegisterEndDate
    {

        [Display(Name = "Altı Aylık")]
        AltiAylik = 1,

        [Display(Name = "Bir Yıllık")]
        BirYillik = 2,

    }

    public class RoleUpdateModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}