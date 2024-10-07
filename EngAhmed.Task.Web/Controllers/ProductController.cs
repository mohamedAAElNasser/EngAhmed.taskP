using EngAhmed.TaskP.Web.Models.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EngAhmed.TaskP.Web.Controllers
{
    public class ProductController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var _data = await _client.GetFromJsonAsync<List<ProductVm>>("Product/GetAll");
            return View(_data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewProductVm obj)
        {
            if (ModelState.IsValid)
            { 
                var _productCreated = await _client.PostAsJsonAsync("Product/Create",obj);
                if (_productCreated.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(obj);
            }
            return View(obj);


        }
        public async Task<IActionResult> Edit (int id)
        {
            var _productTarget = await _client.GetFromJsonAsync<ProductVm>($"Product/GetById?id={id}");
            return View(_productTarget);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit (ProductVm obj)
        {
            if (ModelState.IsValid)
            {
                var _productUpdated = await _client.PutAsJsonAsync("Product/Update", obj);
                if (_productUpdated.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(obj);
            }
            return View(obj);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var _productTarget = await _client.GetFromJsonAsync<ProductVm>($"Product/GetById?id={id}");
            return View(_productTarget);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {

            var _productDeleted = await _client.DeleteAsync($"Product/Delete?id={id}");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PrintReport()
        {
            var pdfBytes = await _client.GetByteArrayAsync("Product/GetProductReport");
            return File(pdfBytes, "application/pdf");
        }
        public async Task<IActionResult> PrintReportDevExpress()
        {
            var pdfBytes = await _client.GetByteArrayAsync("Product/GetProductReportDevExpress");
            return File(pdfBytes, "application/pdf");
        }
        public async Task<IActionResult> DownloadReport()
        {
            var products = await _client.GetFromJsonAsync<List<ProductVm>>("Product/GetAll");

            
            var pdfBytes = await _client.GetByteArrayAsync("Product/GetProductReport");

            return File(pdfBytes, "application/pdf", "ProductReport.pdf");
        }

        public async Task<IActionResult> DownloadExcelReport()
        {
            var products = await _client.GetFromJsonAsync<List<ProductVm>>("Product/GetAll");

            var excelBytes = await _client.GetByteArrayAsync("Product/GenerateDevExpressProductReportExcel");

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductReport.xlsx");
        }

        public async Task<IActionResult> DownloadWordReport()
        {
            var products = await _client.GetFromJsonAsync<List<ProductVm>>("Product/GetAll");

            var wordBytes = await _client.GetByteArrayAsync("Product/GenerateDevExpressProductReportWord");

            return File(wordBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ProductReport.docx");
        }



    }
}
