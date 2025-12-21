using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortal.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir")]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(100)]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Ad Soyad")]
        public string? AdSoyad { get; set; }

        [StringLength(20)]
        [Display(Name = "Öğrenci Numarası")]
        public string? OgrenciNumarasi { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public bool Aktif { get; set; } = true;
    }
}







