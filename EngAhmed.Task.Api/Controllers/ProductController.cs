using EngAhmed.TaskP.Application.Contracts.IAppService;
using EngAhmed.TaskP.Application.Dto.DProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngAhmed.TaskP.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppServiceAsync _ser;
        private readonly IProductDevExpressReportService _devExpSer;

        public ProductController(IProductAppServiceAsync ser, IProductDevExpressReportService devExpSer)
        {
            _ser = ser;
            _devExpSer = devExpSer;
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAll()
        {
            var _data = await _ser.GetAllProductsAsync();
            return _data;
        }
        [HttpGet]
        public async Task<ProductDto> GetById(int id)
        {
            var _data = await _ser.GetProductByIdAsync(id);
            return _data;
        }
        [HttpGet]
        public async Task<List<ProductDto>> GetAllName(string name)
        {
            var _data = await _ser.GetProductByNameAsync(name);
            return _data;
        }
        [HttpDelete]
        public async Task<ProductDto> Delete(int id)
        {
            var _data = await _ser.DeleteProductAsync(id);
            return _data;
        }
        [HttpPost]
        public async Task<ProductDto> Create(NewProductDto obj)
        {
            if (ModelState.IsValid)
            {
                var _data = await _ser.CreateProductAsync(obj);
                return _data;
            }
            return new ProductDto();

        }
        [HttpPut]
        public async Task<ProductDto> Update(ProductDto obj)
        {
            if (!ModelState.IsValid)
                return new ProductDto();
            var _data = await _ser.UpdateProductAsync(obj);
            return _data;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductReport()
        {
            var products = await _ser.GetAllProductsAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("No products available.");
            }

            var pdfBytes = _ser.GenerateProductReport(products);
            var _currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var _fileName = $"ProductReport_{_currentDateTime}.pdf";
            return File(pdfBytes, "application/pdf", _fileName);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductReportDevExpress()
        {
            var products = await _ser.GetAllProductsAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("No products available.");
            }

            var pdfBytes = _devExpSer.GenerateDevExpressProductReport(products,"pdf");
            var _currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var _fileName = $"ProductDevExpressReport_{_currentDateTime}.pdf";
            return File(pdfBytes, "application/pdf", _fileName);
        }
        [HttpGet]
        public async Task<IActionResult> GenerateDevExpressProductReportExcel()
        {
            var products = await _ser.GetAllProductsAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("No products available.");
            }

            var excelBytes = _devExpSer.GenerateDevExpressProductReport(products, "Excel");
            var _currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var _fileName = $"ProductDevExpressReport_{_currentDateTime}.xlsx";
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _fileName);
        }
        [HttpGet]
        public async Task<IActionResult> GenerateDevExpressProductReportWord()
        {
            var products = await _ser.GetAllProductsAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("No products available.");
            }

            var wordBytes = _devExpSer.GenerateDevExpressProductReport(products, "doc");
            var _currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var _fileName = $"ProductDevExpressReport_{_currentDateTime}.docx";
            return File(wordBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", _fileName);
        }
    }
}
