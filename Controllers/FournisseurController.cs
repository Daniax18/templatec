#pragma warning disable 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Template.Models;

namespace Template.Controllers
{
    public class FournisseurController : Controller
    {

        private readonly StoreContext _storeContext;

        public FournisseurController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AutocompleteFournisseur(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return Json(new { results = new string[0] });
            }

            var fournisseurs = _storeContext.Fournisseurs
                 .Where(f => f.Nom.Contains(term)) // LIKE %term%
                 .Select(f => new { id = f.Id, nom = f.Nom }) // Retourner uniquement les champs nécessaires
                 .ToList();

            return Json(fournisseurs); // Retourne un tableau JSON
        }
    }
}
