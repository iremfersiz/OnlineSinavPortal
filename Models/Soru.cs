using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSinavPortal.Models
{
    public class Soru
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Soru metni zorunludur")]
        [StringLength(1000, ErrorMessage = "Soru metni en fazla 1000 karakter olabilir")]
        [Display(Name = "Soru Metni")]
        [DataType(DataType.MultilineText)]
        public string SoruMetni { get; set; } = string.Empty;

        [Required(ErrorMessage = "Puan zorunludur")]
        [Display(Name = "Puan")]
        [Range(1, 100, ErrorMessage = "Puan 1-100 arasında olmalıdır")]
        public int Puan { get; set; } = 10;

        [Display(Name = "Sıra")]
        public int Sira { get; set; } = 1;

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Sınav")]
        public int SinavId { get; set; }

        [ForeignKey("SinavId")]
        public virtual Sinav? Sinav { get; set; }

        public virtual ICollection<Secenek> Secenekler { get; set; } = new List<Secenek>();
    }
}







