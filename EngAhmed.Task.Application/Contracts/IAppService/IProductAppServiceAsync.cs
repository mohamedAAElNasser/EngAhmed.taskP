using EngAhmed.TaskP.Application.Dto.DCustomer;
using EngAhmed.TaskP.Application.Dto.DProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Application.Contracts.IAppService
{
    public interface IProductAppServiceAsync
    {
        Task <ProductDto> CreateProductAsync(NewProductDto obj);
        Task<ProductDto> UpdateProductAsync(ProductDto obj);
        Task<ProductDto> DeleteProductAsync(int id);
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetProductByNameAsync(string name);
        byte[] GenerateProductReport(List<ProductDto> products);
    }
}
