using EngAhmed.TaskP.Application.Dto.DCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Application.Contracts.IRepostories
{
    public interface ICustomerRepository
    {
        Task<CustomerDto> GetByIdAsync(int id);
        Task<List<CustomerDto>> GetByNameAsync(string name);
    }
}
