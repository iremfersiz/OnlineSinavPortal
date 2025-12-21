using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSinavPortal.Models
{
    public class Sinav
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Sınav başlığı zorunludur")]
        [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir")]
        [Display(Name = "Sınav Başlığı")]
        public string Baslik { get; set; } = string.Empty;

        [StringLength(1000)]
        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string? Aciklama { get; set; }

        [Required(ErrorMessage = "Süre zorunludur")]
        [Display(Name = "Süre (Dakika)")]
        [Range(1, 600, ErrorMessage = "Süre 1-600 dakika arasında olmalıdır")]
        public int Sure { get; set; }

        [Display(Name = "Toplam Puan")]
        [Range(0, 1000, ErrorMessage = "Toplam puan 0-1000 arasında olmalıdır")]
        public int ToplamPuan { get; set; } = 100;

        [Display(Name = "Geçme Notu")]
        [Range(0, 100, ErrorMessage = "Geçme notu 0-100 arasında olmalıdır")]
        public int GecmeNotu { get; set; } = 50;

        [Display(Name = "Aktif")]
        public bool Aktif { get; set; } = true;

        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime? BaslangicTarihi { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime? BitisTarihi { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Oluşturan Kullanıcı")]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        public virtual ICollection<Soru> Sorular { get; set; } = new List<Soru>();
    }
}







