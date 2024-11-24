using Microsoft.AspNetCore.Mvc;
using Template.Models;

namespace Template.Controllers
{
    public class ClientController : BaseController
    {

        private readonly StoreContext storeContext;

        public ClientController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        [HttpGet]
        public JsonResult AutoCompleteClient(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return Json(new { results = new string[0] });
            }

            var clients = storeContext.Clients
                .Where(c => c.Nom.Contains(term) || c.Id.Contains(term))
                .Select(c => new { id = c.Id, nom = c.Nom })
                .ToList();
            return Json(clients);
        }
    }
}
