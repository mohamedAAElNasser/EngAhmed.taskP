using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http.Headers;

namespace EngAhmed.TaskP.Web.Controllers
{
    public class BaseController : Controller
    {
        public HttpClient _client = new HttpClient();
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _client.BaseAddress = new Uri("http://localhost:5244/api/");
            var token = HttpContext.Session.GetString("Token");

            // تحقق من وجود الـ Token واستبعاد صفحة تسجيل الدخول
            if (string.IsNullOrEmpty(token) && context.RouteData.Values["action"].ToString() != "Login")
            {
                context.Result = RedirectToAction("Login", "Account");
            }
            else if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            base.OnActionExecuting(context);
        }
    }
}
