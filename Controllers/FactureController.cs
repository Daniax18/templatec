﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Template.Models;
using Template.Models.invoice;

namespace Template.Controllers
{
    public class FactureController : Controller
    {

        private readonly StoreContext _storeContext;

        public FactureController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }


        [HttpPost]
        public ActionResult Exporter(string format)
        {
            // Récupérez la liste des livraisons
            List<VFactureComplet> factures = _storeContext.VFactureComplets.ToList();

            switch (format.ToLower())
            {
                case "pdf":
                    // Implémentez la génération de PDF (ex. : avec iTextSharp ou un autre outil)
                    byte[] pdfBytes = VFactureComplet.GeneratePdf(factures);
                    return File(pdfBytes, "application/pdf", "factures.pdf");
                case "excel":
                    byte[] excelBytes = VFactureComplet.GenerateExcel(factures);
                    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "factures.xlsx");
                case "csv":
                    byte[] csvBytes = VFactureComplet.GenerateCsv(factures);
                    return File(csvBytes, "text/csv", "factures.csv");
                default:
                    return RedirectToAction("Index"); // En cas de format invalide
            }
        }


        // PAGE ACCEUIL
        public IActionResult Index()
        {
            ViewBag.Magasins = new SelectList(_storeContext.Magasins, "Id", "Nom");
            ViewBag.NbrLigne = 5;
            return View("InsertFacture");
        }

        // LISTE FACTURE
        public IActionResult ListeFacture()
        {
            var lists = _storeContext.VFactureComplets.ToList();
            ViewBag.Factures = lists;
            return View();
        }

        // INSERTION CONTROLLER
        public IActionResult CreateFacture()
        {
            // Mere
            string date = Request.Form["date"];
            string idmagasin = Request.Form["idmagasin"];
            string idclient = Request.Form["idclient"];
            int nbrLigne = int.Parse(Request.Form["nbr_ligne"]);
            string remarque = Request.Form["remarque"];

            var facture = new Facture
            {
                Id = "F1",
                DateF = DateOnly.Parse(date),
                Idmagasin = idmagasin,
                Idclient = idclient,
                Remarque = remarque
            };

            List<Detailfacture> filles = new List<Detailfacture>();
            decimal? somme = 0;

            for (int i = 0; i < nbrLigne; i++)
            {
                if (Request.Form[$"check_{i}"] == "on")
                {
                    int qty = int.Parse(Request.Form[$"qty_{i}"]);
                    string idproduit = Request.Form[$"idproduit_{i}"];
                    Detailfacture temp = new Detailfacture(facture.Id, idproduit, qty, _storeContext);
                    filles.Add(temp);
                    somme += temp.TotalVente;
                }
            }
            facture.TotalFacture = somme;

            using (var transaction = _storeContext.Database.BeginTransaction())
            {
                try
                {
                    _storeContext.Factures.Add(facture);
                    _storeContext.SaveChanges();

                    _storeContext.Detailfactures.AddRange(filles);
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

                    throw new Exception($"Error on inserting factrure: {detailedError}\nStack Trace: {ex.StackTrace}");
                }
            }

            return RedirectToAction("Index", "Facture");
        }
    }
}
