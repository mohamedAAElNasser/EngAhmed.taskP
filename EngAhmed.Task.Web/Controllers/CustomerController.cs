using EngAhmed.TaskP.Web.Models.ViewModels.CustomerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EngAhmed.TaskP.Web.Controllers
{
    public class CustomerController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var _data = await _client.GetFromJsonAsync<List<CustomerVm>>("Customer/GetAll");
            return View(_data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewCustomerVm obj)
        {
            if (ModelState.IsValid)
            {
                var _CustomerCreated = await _client.PostAsJsonAsync("Customer/Create", obj);
                if (_CustomerCreated.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(obj);
            }
            return View(obj);


        }
        public async Task<IActionResult> Edit(int id)
        {
            var _CustomerTarget = await _client.GetFromJsonAsync<CustomerVm>($"Customer/GetById?id={id}");
            return View(_CustomerTarget);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerVm obj)
        {
            if (ModelState.IsValid)
            {
                var _CustomerUpdated = await _client.PutAsJsonAsync("Customer/Update", obj);
                if (_CustomerUpdated.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(obj);
            }
            return View(obj);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var _CustomerTarget = await _client.GetFromJsonAsync<CustomerVm>($"Customer/GetById?id={id}");
            return View(_CustomerTarget);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {

            var _CustomerDeleted = await _client.DeleteAsync($"Customer/Delete?id={id}");
            return RedirectToAction("Index");
        }
    }
}
