using EngAhmed.TaskP.Application.Dto.DCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Application.Contracts.IAppService
{
    public interface ICustomerAppServiceAsync
    {
        Task<CustomerDto> CreateCustomerAsync(NewCustomerDto obj);
        Task<CustomerDto> UpdateCustomerAsync(CustomerDto obj);
        Task<CustomerDto> DeleteCustomerAsync(int id);
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<List<CustomerDto>> GetCustomersByNameAsync(string name);
    }
}
