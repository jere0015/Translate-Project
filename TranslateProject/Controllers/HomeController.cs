using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TranslateProject.Models;
using TranslateProject.Services;

namespace TranslateProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITranslationService _translationService;
        public HomeController(ILogger<HomeController> logger, ITranslationService translationService)
        {
            _logger = logger;
            _translationService = translationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<ActionResult> Translate(Translation translation)
        {
            if (translation.Text == null)
            {
                return BadRequest("Error. No text detected.");
            }
            string? translationResult = await _translationService.TranslateAsync(translation);

            if (translationResult == null)
            {
                return Content("Error. Not translatable, or error during translation.");
            }

            return Content($"{translationResult}");
        }
    }
}