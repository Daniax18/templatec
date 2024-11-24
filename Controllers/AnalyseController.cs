using Microsoft.AspNetCore.Mvc;
using Template.Models;
using Template.Models.analyse;

namespace Template.Controllers
{
    public class AnalyseController : Controller
    {
        private readonly StoreContext _context;

        public AnalyseController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult DataAnalyseMagasin()
        {
            // Requête LINQ pour obtenir les données groupées par "Nom" avec la somme des "Total"
            var data = _context.VFactureComplets
             .GroupBy(m => m.MNom) // Grouper par le nom du magasin (MNom)
             .Select(g => new
             {
                 label = g.Key, // Clé du groupe (Nom du magasin)
                 value = g.Sum(m => m.TotalFacture), // Somme des valeurs "TotalFacture" dans le groupe
                 color = $"#{new Random().Next(0x1000000):X6}" // Génère une couleur aléatoire
             })
             .ToList();

            return Json(data);
        }

        [HttpGet]
        public IActionResult DataAnalyseMagasinFiltre(string? startDate, string? endDate, string? magasin)
        {

            DateOnly start = startDate != null ? DateOnly.Parse(startDate) : DateOnly.MinValue;
            DateOnly end = endDate != null ? DateOnly.Parse(endDate) : DateOnly.MaxValue;
            string mag = magasin != null ? magasin : "";

            var data = _context.VFactureComplets
             .Where(m => m.DateF >= start && m.DateF <= end && m.MNom.Contains(mag))
             .GroupBy(m => m.MNom)
             .Select(g => new
             {
                 label = g.Key,
                 value = g.Sum(m => m.TotalFacture),
                 color = $"#{new Random().Next(0x1000000):X6}"
             })
             .ToList();

            return Json(data);
        }

        public IActionResult AnalyseMagasin(string? date1 = "", string? date2 = "", string? magasin = "")
        {

            ViewBag.Date1 = string.IsNullOrEmpty(date1) ? "" : date1;
            ViewBag.Date2 = string.IsNullOrEmpty(date2) ? "" : date2;
            ViewBag.Magasin = string.IsNullOrEmpty(magasin) ? "" : magasin;

            return View("Chart");
        }

        public IActionResult AnalyseCategorie()
        {
            return View("Donut");
        }

        [HttpGet]
        public IActionResult DataAnalyseCategorie()
        {
            List<VAnalyseCategorie> cat = _context.VAnalyseCategories.ToList();


            // Transformez les données en un objet JSON compatible avec Chart.js
            var chartData = new
            {
                labels = cat.Select(d => d.Nom).ToList(), // Liste des noms
                datasets = new[]
                {
                    new
                    {
                        data = cat.Select(d => d.Total).ToList(), // Liste des totaux
                        fillColor= "rgba(54, 162, 235, 0.6)",
                        strokeColor= "rgba(210, 214, 222, 1)",
                        pointColor= "rgba(210, 214, 222, 1)",
                        pointStrokeColor= "#c1c7d1",
                        pointHighlightFill= "#fff",
                        pointHighlightStroke= "rgba(220,220,220,1)"
                    }
                }
            };
            return Json(chartData);
        }
    }
}
