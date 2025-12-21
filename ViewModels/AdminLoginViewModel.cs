using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortal.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Öğrenci numarası zorunludur")]
        [StringLength(20, ErrorMessage = "Öğrenci numarası en fazla 20 karakter olabilir")]
        [Display(Name = "Öğrenci Numarası")]
        public string OgrenciNumarasi { get; set; } = string.Empty;

        [Display(Name = "Beni Hatırla")]
        public bool BeniHatirla { get; set; }
    }
}







