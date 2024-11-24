using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Template.Models;

namespace Template.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StoreContext _context;

        public HomeController(ILogger<HomeController> logger, StoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Unities = new SelectList(_context.Unites, "Id", "Nom");
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Nom");

            return View();
        }

        public IActionResult Acceuil()
        {
            return View("HomeView");
        }

        public IActionResult Graph()
        {
            return View();
        }
        public IActionResult Multiple()
        {
            return View();
        }

        public ActionResult _Error()
        {
            return View();
        }
    }
}
