#pragma warning disable 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Template.Models;
using Template.Models.apj.histo;
using Template.Models.produit;
using Template.Utils;

namespace Template.Controllers
{
    public class ProduitController : BaseController
    {
        private readonly StoreContext _storeContext;

        public ProduitController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpPost]
        public IActionResult CreateProduit()
        {
            // Récupérer les données depuis Request.Form
            string? designation = Request.Form["designation"];
            string? description = Request.Form["description"];
            decimal? pu = decimal.Parse(Request.Form["pu"]);
            string? idUnite = Request.Form["idunite"];
            string? idCategorie = Request.Form["idcategorie"];

            var nextSequenceValue = Utilities.GetNextSequenceAsync("ProduitSequence", _storeContext);


            Produit produit = new Produit
            {
                Id = $"PROD{nextSequenceValue}",
                Nom = designation,
                Idcategorie = idCategorie,
                Idunite = idUnite,
                Pv = pu * 3,
                Pu = pu
            };

            string[] extracted = Utilities.ExtractEntityProperties(produit);
            Type type = produit.GetType();

            var histo = new Historique
            {
                Idhistorique = designation,
                Datehistorique = DateOnly.FromDateTime(DateTime.Now),
                Heure = DateTime.Now.ToString("HH:mm:ss"),
                Objet = type.FullName,
                Action = "insertion",
                Idutilisateur = HttpContext.Session.GetString("UserId"),
                Refobjet = type.Name
            };

            HistoriqueValeur histo_valeur = new HistoriqueValeur
            {
                Id = designation,
                Idhisto = histo.Idhistorique,
                Refhisto = $"{histo.Datehistorique}_{histo.Idhistorique}",
                NomTable = type.Name,
                NomClasse = type.FullName
            };
            for (int i = 0; i < (extracted.Length / 2); i++)
            {
                typeof(HistoriqueValeur).GetProperty($"Val{i + 1}")?.SetValue(histo_valeur, extracted[i]);
            }


            using (var transaction = _storeContext.Database.BeginTransaction())
            {
                try
                {
                    _storeContext.Produits.Add(produit);
                    _storeContext.SaveChanges();
                    _storeContext.Historiques.Add(histo);
                    _storeContext.SaveChanges();

                    _storeContext.HistoriqueValeurs.Add(histo_valeur);
                    _storeContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ViewBag.Error = ex.ToString();
                    Console.Write(ex.ToString());
                    return RedirectToAction("_Error", "Home");
                }

            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult AutoCompleteProduit(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return Json(new { results = new string[0] });
            }

            var result = _storeContext.VProduitComplets
                 .Where(f => f.Nom.Contains(term)) // LIKE %term%
                 .Select(f => new
                 {
                     id = f.Id,
                     nom = f.Nom,
                     unite = f.UniteNom
                 }) // Retourner uniquement les champs nécessaires
                 .ToList();

            return Json(result); // Retourne un tableau JSON
        }
    }
}
