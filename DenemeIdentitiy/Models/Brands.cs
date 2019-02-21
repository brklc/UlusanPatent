using DenemeIdentitiy.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DenemeIdentitiy.Models
{
    public class Brands
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name ="Lisans No")]
        public string LicenseNumber { get; set; }

        [Display(Name = "Üretim/İthal")]
        public ProductType? ProductTypes { get; set; }

        [Display(Name ="Lisans Türü")]
        public string LicenseType { get; set; }

        [Display(Name = "Tescil Numarası")]
        public string RegistrationNo { get; set; }

        [Display(Name ="Veriliş Nedeni")]
        public string ReasonForIssue { get; set; }

        [Display(Name = "Veriliş Tarihi")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Ürün Adı")]
        public string BrandName { get; set; }

        [Display(Name = "Gübre Tip Adı")]
        public string TypeName { get; set; }

        [Display(Name = "Tescil Belgesi")]
        [DataType(DataType.Upload)]
        public string RegistrationPhoto { get; set; }

        [Display(Name ="Açıklama")]
        public string Description { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Başvuru Durumu")]
        public Statuses? Status { get; set; }

    }

    public enum Statuses
    {

        [Display(Name = "Başvurunuz Yapılmıştır.")]
        BasvuruYapildi = 1,

        [Display(Name = "Başvurunuz İlgili Uzamana Sevk Edilmiştir.")]
        BasvuruUzman = 2,

        [Display(Name = "Başvurunuzda Eksik Çıkmıştır.")]
        BasvuruEksik = 3,

        [Display(Name = "Başvurunuz Onay İmza Aşamasındadır.")]
        BasvuruImzaAsamasi = 4,

        [Display(Name = "Başvurunuz Onaylanmıştır.")]
        BasvuruOnay = 5

    }

    public enum ProductType
    {

        [Display(Name = "İthal")]
        Ithal = 1,

        [Display(Name = "Üretim")]
        Uretim = 2,
    }
}