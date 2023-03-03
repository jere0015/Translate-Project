using System.ComponentModel.DataAnnotations;

namespace TranslateProject.Models
{
    public class Translation
    {
        [Required]
        public string? Text { get; set; }
        public string? TargetLanguage { get; set; }
        public string? ProviderName { get; set; }
    }
}
