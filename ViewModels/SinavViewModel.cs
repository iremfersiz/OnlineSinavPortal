using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortal.ViewModels
{
    public class SinavViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sınav başlığı zorunludur")]
        [StringLength(200)]
        [Display(Name = "Sınav Başlığı")]
        public string Baslik { get; set; } = string.Empty;

        [StringLength(1000)]
        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string? Aciklama { get; set; }

        [Required(ErrorMessage = "Süre zorunludur")]
        [Display(Name = "Süre (Dakika)")]
        [Range(1, 600)]
        public int Sure { get; set; }

        [Display(Name = "Toplam Puan")]
        [Range(0, 1000)]
        public int ToplamPuan { get; set; } = 100;

        [Display(Name = "Geçme Notu")]
        [Range(0, 100)]
        public int GecmeNotu { get; set; } = 50;

        [Display(Name = "Aktif")]
        public bool Aktif { get; set; } = true;

        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime? BaslangicTarihi { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime? BitisTarihi { get; set; }

        public int SoruSayisi { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
    }
}






