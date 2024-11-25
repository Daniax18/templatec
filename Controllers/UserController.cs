using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Template.Models;
using Template.Models.apj.user;

namespace Template.Controllers
{
    public class UserController : Controller
    {

        private readonly StoreContext _context;

        public UserController(StoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Login";
            ViewBag.Magasins = new SelectList(_context.Magasins, "Id", "Nom");

            return View("Login");
        }

        [HttpPost]
        public IActionResult Login()
        {
            string? nomUser = Request.Form["nom"];
            string? pwd = Request.Form["pwduser"];
            string? magasin = Request.Form["idmagasin"];

            if (!string.IsNullOrEmpty(nomUser) && !string.IsNullOrEmpty(pwd))
            {
                var user_role = _context.VUtilisateurRoles
                    .FirstOrDefault(u => u.Nomuser == nomUser);

                if (user_role != null)
                {
                    VUtilisateurMagasinLib? check_user = null;
                    if (user_role.Rang > 1)
                    {
                        check_user = _context.VUtilisateurMagasinLibs
                            .FirstOrDefault(u => u.Nomuser == nomUser && u.Pwduser == pwd);
                    }
                    else
                    {
                        check_user = _context.VUtilisateurMagasinLibs
                           .FirstOrDefault(u => u.Nomuser == nomUser && u.Pwduser == pwd && u.MagasinId == magasin);
                    }
                    if (check_user != null)
                    {
                        HttpContext.Session.SetString("UserName", check_user.Nomuser);
                        HttpContext.Session.SetString("UserRef", check_user.Refuser.ToString());
                        HttpContext.Session.SetString("UserId", check_user.Id.ToString());
                        HttpContext.Session.SetString("UserRole", user_role.Rang.ToString());
                        return RedirectToAction("Acceuil", "Home");
                    }
                    else
                    {
                        // Message d'erreur : Identifiants invalides
                        TempData["ErrorMessage"] = "User, mot de passe ou magasin invalide.";
                        return RedirectToAction("Index", "User");
                    }
                }
            }
            TempData["ErrorMessage"] = "User, mot de passe ou magasin invalide.";
            return RedirectToAction("Index", "User");
        }

        public IActionResult Logout()
        {
            // Supprimer toutes les données de la session
            HttpContext.Session.Clear();

            // Rediriger vers la page de connexion
            return RedirectToAction("Index", "User");
        }
    }
}
