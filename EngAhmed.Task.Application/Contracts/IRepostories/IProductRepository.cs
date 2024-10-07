using EngAhmed.TaskP.Application.Dto.DProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Application.Contracts.IRepostories
{
    public interface IProductRepository
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<List<ProductDto>> GetByNameAsync(string name);


    }
}
