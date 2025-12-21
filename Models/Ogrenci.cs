using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortal.Models
{
    public class Ogrenci
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        [StringLength(100)]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Öğrenci numarası zorunludur")]
        [StringLength(20)]
        [Display(Name = "Öğrenci Numarası")]
        public string OgrenciNumarasi { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(100)]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Aktif")]
        public bool Aktif { get; set; } = true;

        // Navigation properties
        public virtual ICollection<SinavSonucu> SinavSonuclari { get; set; } = new List<SinavSonucu>();
    }
}

