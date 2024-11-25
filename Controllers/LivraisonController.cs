#pragma warning disable 

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Template.Models;
using Template.Models.livraison;
using Template.Utils;

namespace Template.Controllers
{
    public class LivraisonController : BaseController
    {

        private readonly StoreContext _storeContext;

        public LivraisonController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }



        // EXPORT PDF - EXCEL - CSV
        [HttpPost]
        public ActionResult Exporter(string format)
        {
            // Récupérez la liste des livraisons
            List<VLivraisonComplet> livraisons = _storeContext.VLivraisonComplets.ToList();

            switch (format.ToLower())
            {
                case "pdf":
                    // Implémentez la génération de PDF (ex. : avec iTextSharp ou un autre outil)
                    byte[] pdfBytes = VLivraisonComplet.GeneratePdf(livraisons);
                    return File(pdfBytes, "application/pdf", "livraisons.pdf");
                case "excel":
                    byte[] excelBytes = VLivraisonComplet.GenerateExcel(livraisons);
                    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "livraisons.xlsx");
                case "csv":
                    byte[] csvBytes = VLivraisonComplet.GenerateCsv(livraisons);
                    return File(csvBytes, "text/csv", "livraisons.csv");
                default:
                    return RedirectToAction("Index"); // En cas de format invalide
            }
        }

        // FICHE LIVRAISON AVEC FILLES
        public IActionResult DetailLivraison(string id)
        {
            var livraison = _storeContext.VLivraisonComplets
                .FirstOrDefault(l => l.Id == id);

            List<VDetailLivraisonComplet> filles = null;

            if (livraison != null)
            {
                filles = _storeContext.VDetailLivraisonComplets
                    .Where(l => l.Idlivraison == id)
                    .ToList();
            }
            ViewBag.Livraison = livraison;
            ViewBag.Filles = filles;
            return View("FicheLivraison");
        }

        // PAGE LISTE LIVRAISONS
        public IActionResult ListeLivraison()
        {
            var ls = _storeContext.VLivraisonComplets.ToList();
            ViewBag.Livraisons = ls;
            return View("ListeLivraison");
        }

        // PAGE AJOUT MULTIPLE
        public IActionResult Index()
        {
            ViewBag.Magasins = new SelectList(_storeContext.Magasins, "Id", "Nom");
            ViewBag.NbrLigne = 5;
            return View("Livraison");
        }

        // AJOUT SIMPLE LIVRAISON
        public IActionResult CreateLivraison()
        {
            string id = Request.Form["id"];
            string idbc = Request.Form["idbc"];
            string date = Request.Form["date"];
            string idmagasin = Request.Form["idmagasin"];
            string idfrn = Request.Form["idfrn"];

            var nextSequenceValue = Utilities.GetNextSequenceAsync("LivraisonSequence", _storeContext);
            var livraison = new Livraison
            {
                Id = $"LIV{nextSequenceValue}",
                Date = DateOnly.Parse(date),
                Idmagasin = idmagasin,
                Idboncommande = idbc,
                Idfournisseur = idfrn
            };

            try
            {
                _storeContext.Livraisons.Add(livraison);
                _storeContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Include the inner exception and full stack trace for detailed logging
                var detailedError = ex.InnerException != null
                    ? $"{ex.Message} | Inner Exception: {ex.InnerException.Message}"
                    : ex.Message;

                throw new Exception($"Error on inserting livraison: {detailedError}\nStack Trace: {ex.StackTrace}");
            }

            return RedirectToAction("Index", "Livraison");
        }

        // AJOUT AVEC MULTIPLE
        public IActionResult CreateMultipleLivraison()
        {

            // Mere
            string idbc = Request.Form["idbc"];
            string date = Request.Form["date"];
            string idmagasin = Request.Form["idmagasin"];
            string idfrn = Request.Form["idfrn"];
            int nbrLigne = int.Parse(Request.Form["nbr_ligne"]);
            var nextSequenceValue = Utilities.GetNextSequenceAsync("LivraisonSequence", _storeContext);

            var livraison = new Livraison
            {
                Id = $"LIV{nextSequenceValue}",
                Date = DateOnly.Parse(date),
                Idmagasin = idmagasin,
                Idboncommande = idbc,
                Idfournisseur = idfrn
            };

            List<Detaillivraison> filles = new List<Detaillivraison>();
            for (int i = 0; i < nbrLigne; i++)
            {
                if (Request.Form[$"check_{i}"] == "on")
                {
                    decimal qty = decimal.Parse(Request.Form[$"qty_{i}"]);
                    string idproduit = Request.Form[$"idproduit_{i}"];

                    filles.Add(new Detaillivraison(livraison.Id, $"dl_{livraison.Id}_{i}", idproduit, qty, _storeContext));
                }
            }

            using (var transaction = _storeContext.Database.BeginTransaction())
            {
                try
                {
                    _storeContext.Livraisons.Add(livraison);
                    _storeContext.SaveChanges();

                    _storeContext.Detaillivraisons.AddRange(filles);
                    _storeContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Include the inner exception and full stack trace for detailed logging
                    var detailedError = ex.InnerException != null
                        ? $"{ex.Message} | Inner Exception: {ex.InnerException.Message}"
                        : ex.Message;

                    throw new Exception($"Error on inserting livraison: {detailedError}\nStack Trace: {ex.StackTrace}");
                }
            }


            return RedirectToAction("Index", "Livraison");
        }
    }
}
