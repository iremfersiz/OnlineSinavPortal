using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortal.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        [Display(Name = "Ad Soyad")]
        public string? AdSoyad { get; set; }

        [StringLength(20)]
        [Display(Name = "Öğrenci Numarası")]
        public string? OgrenciNumarasi { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Aktif")]
        public bool Aktif { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Sinav> OlusturduguSinavlar { get; set; } = new List<Sinav>();
        public virtual ICollection<SinavSonucu> SinavSonuclari { get; set; } = new List<SinavSonucu>();
    }
}

