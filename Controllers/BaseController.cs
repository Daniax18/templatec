using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Template.Controllers
{
    public abstract class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Vérifiez si la session "UserName" existe
            if (HttpContext.Session.GetString("UserId") == null)
            {
                context.Result = new RedirectToActionResult("Index", "User", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
