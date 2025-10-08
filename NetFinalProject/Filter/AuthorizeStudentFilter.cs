using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetFinalProject.Filters
{
    public class AuthorizeStudentFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loggedIn = context.HttpContext.Session.GetString("LoggedIn");
            if (string.IsNullOrEmpty(loggedIn))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
