using System.ComponentModel.DataAnnotations;

namespace TranslateProject.Models
{
    public enum Providers
    {
        [Display(Name = "NLP Translation")]
        Nlp,
        DeepL,
        Microsoft
    }
}
