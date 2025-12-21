using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSinavPortal.Models
{
    public class SinavSonucu
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Sınav")]
        public int SinavId { get; set; }

        [ForeignKey("SinavId")]
        public virtual Sinav? Sinav { get; set; }

        [Display(Name = "Kullanıcı")]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        [Display(Name = "Öğrenci")]
        public int? OgrenciId { get; set; }

        [ForeignKey("OgrenciId")]
        public virtual Ogrenci? Ogrenci { get; set; }

        [Display(Name = "Alınan Puan")]
        public int AlinanPuan { get; set; }

        [Display(Name = "Toplam Puan")]
        public int ToplamPuan { get; set; }

        [Display(Name = "Doğru Sayısı")]
        public int DogruSayisi { get; set; }

        [Display(Name = "Yanlış Sayısı")]
        public int YanlisSayisi { get; set; }

        [Display(Name = "Boş Sayısı")]
        public int BosSayisi { get; set; }

        [Display(Name = "Başlangıç Zamanı")]
        public DateTime BaslangicZamani { get; set; }

        [Display(Name = "Bitiş Zamanı")]
        public DateTime? BitisZamani { get; set; }

        [Display(Name = "Süre (Dakika)")]
        public int? SureDakika { get; set; }

        [Display(Name = "Sonuç")]
        public bool GectiMi { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Cevaplar JSON olarak saklanabilir veya ayrı bir tablo ile ilişkilendirilebilir
        [Display(Name = "Cevaplar (JSON)")]
        public string? CevaplarJson { get; set; }
    }
}

