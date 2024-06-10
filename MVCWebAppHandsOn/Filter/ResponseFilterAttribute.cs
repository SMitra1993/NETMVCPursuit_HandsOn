using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCWebAppHandsOn.Filter
{
    public class HandleJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Accepted;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
