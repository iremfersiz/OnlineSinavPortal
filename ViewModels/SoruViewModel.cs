using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortal.ViewModels
{
    public class SoruViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Soru metni zorunludur")]
        [StringLength(1000)]
        [Display(Name = "Soru Metni")]
        [DataType(DataType.MultilineText)]
        public string SoruMetni { get; set; } = string.Empty;

        [Required(ErrorMessage = "Puan zorunludur")]
        [Display(Name = "Puan")]
        [Range(1, 100)]
        public int Puan { get; set; } = 10;

        [Display(Name = "Sıra")]
        public int Sira { get; set; } = 1;

        [Display(Name = "Sınav")]
        public int SinavId { get; set; }

        public string? SinavBaslik { get; set; }

        public List<SecenekViewModel> Secenekler { get; set; } = new List<SecenekViewModel>();
    }

    public class SecenekViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Seçenek metni zorunludur")]
        [StringLength(500)]
        [Display(Name = "Seçenek Metni")]
        public string SecenekMetni { get; set; } = string.Empty;

        [Display(Name = "Doğru Cevap")]
        public bool DogruCevap { get; set; } = false;

        [Display(Name = "Sıra")]
        public int Sira { get; set; } = 1;

        public int SoruId { get; set; }
    }
}






