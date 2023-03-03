using TranslateProject.Models;

namespace TranslateProject.Services
{
    public interface ITranslationService
    {
        Task<string> TranslateAsync(Translation translation);
    }
}
