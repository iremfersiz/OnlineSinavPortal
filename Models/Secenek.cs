using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSinavPortal.Models
{
    public class Secenek
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Seçenek metni zorunludur")]
        [StringLength(500, ErrorMessage = "Seçenek metni en fazla 500 karakter olabilir")]
        [Display(Name = "Seçenek Metni")]
        public string SecenekMetni { get; set; } = string.Empty;

        [Display(Name = "Doğru Cevap")]
        public bool DogruCevap { get; set; } = false;

        [Display(Name = "Sıra")]
        public int Sira { get; set; } = 1;

        [Display(Name = "Soru")]
        public int SoruId { get; set; }

        [ForeignKey("SoruId")]
        public virtual Soru? Soru { get; set; }
    }
}







