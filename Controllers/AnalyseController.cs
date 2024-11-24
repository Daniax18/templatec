using Microsoft.AspNetCore.Mvc;
using Template.Models;

namespace Template.Controllers
{
    public class AnalyseController : BaseController
    {
        private readonly StoreContext _context;

        public AnalyseController(StoreContext context)
        {
            _context = context;
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

        public IActionResult AnalyseCategorie(string? date1 = "", string? date2 = "", string? categorie = "")
        {
            ViewBag.Date1 = string.IsNullOrEmpty(date1) ? null : date1;
            ViewBag.Date2 = string.IsNullOrEmpty(date2) ? null : date2;
            ViewBag.Categorie = string.IsNullOrEmpty(categorie) ? null : categorie;
            return View("Donut");
        }

        [HttpGet]
        public IActionResult DataAnalyseCategorie(string? startDate, string? endDate, string? categorie)
        {
            DateOnly start = startDate != null ? DateOnly.Parse(startDate) : DateOnly.MinValue;
            DateOnly end = endDate != null ? DateOnly.Parse(endDate) : DateOnly.MaxValue;
            string cat = categorie != null ? categorie : "";

            var data = _context.VDetailFactureComplets
             .Where(m => m.DateF >= start && m.DateF <= end && m.Catnom.Contains(cat))
             .GroupBy(m => m.Catnom)
             .Select(g => new
             {
                 MNom = g.Key, // Nom du magasin (clé du groupe)
                 Total = g.Sum(m => m.Qte), // Somme des ventes
                 Color = $"#{new Random().Next(0x1000000):X6}" // Couleur aléatoire
             })
             .ToList();

            // Transformez les données en un objet JSON compatible avec Chart.js
            var chartData = new
            {
                labels = data.Select(d => d.MNom).ToList(), // Liste des noms des magasins
                datasets = new[]
                {
                    new
                    {
                        data = data.Select(d => d.Total).ToList(), // Liste des totaux des factures
                        fillColor= "rgba(54, 162, 235, 0.6)",
                        strokeColor= "rgba(210, 214, 222, 1)",
                        pointColor= "rgba(210, 214, 222, 1)",
                        pointStrokeColor= "#c1c7d1",
                        pointHighlightFill= "#fff",
                        pointHighlightStroke= "rgba(220,220,220,1)",
                        borderWidth = 1 // Largeur des bordures
                    }
                }
            };

            return Json(chartData);
        }
    }
}
