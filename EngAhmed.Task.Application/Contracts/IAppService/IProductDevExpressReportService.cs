using EngAhmed.TaskP.Application.Dto.DProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Application.Contracts.IAppService
{
    public interface IProductDevExpressReportService
    {
        byte[] GenerateDevExpressProductReport(List<ProductDto> products, string type);
        
    }
}
