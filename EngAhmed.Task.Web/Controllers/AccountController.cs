using EngAhmed.TaskP.Web.Models.ViewModels.IdentityViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EngAhmed.TaskP.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Logon
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm obj)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.PostAsJsonAsync("Account", obj);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<CustomTokenVm>(jsonResponse);

                    HttpContext.Session.SetString("Token", tokenResponse.token);
                    HttpContext.Session.SetString("UserName", tokenResponse.userName);
                    HttpContext.Session.SetString("UserId", tokenResponse.userId);
                    HttpContext.Session.SetString("Expiration", tokenResponse.expiration.ToString());

                   
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
                    return View(obj);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
                return View(obj);
            }



        }
#endregion
        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Expiration");

            return RedirectToAction("Login", "Account"); 
        }
        #endregion
    }
}
