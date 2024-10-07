using EngAhmed.TaskP.Application.Contracts.IAppService;
using EngAhmed.TaskP.Application.Dto.DCustomer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngAhmed.TaskP.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppServiceAsync _ser;

        public CustomerController(ICustomerAppServiceAsync ser)
        {
            _ser = ser;
        }

        [HttpGet]
        public async Task<List<CustomerDto>> GetAll()
        {
            var _data = await _ser.GetAllCustomersAsync();
            return _data;
        }
        [HttpGet]
        public async Task<CustomerDto> GetById(int id)
        {
            var _data = await _ser.GetCustomerByIdAsync(id);
            return _data;
        }
        [HttpGet]
        public async Task<List<CustomerDto>> GetAllName(string name)
        {
            var _data = await _ser.GetCustomersByNameAsync(name);
            return _data;
        }
        [HttpDelete]
        public async Task<CustomerDto> Delete(int id)
        {
            var _data = await _ser.DeleteCustomerAsync(id);
            return _data;
        }
        [HttpPost]
        public async Task<CustomerDto> Create(NewCustomerDto obj)
        {
            if (ModelState.IsValid)
            { 
                var _data = await _ser.CreateCustomerAsync(obj);
                return _data;
            }
            return new CustomerDto();

        }
        [HttpPut]
        public async Task<CustomerDto> Update(CustomerDto obj)
        {
            if (!ModelState.IsValid)
                return new CustomerDto();
            var _data = await _ser.UpdateCustomerAsync(obj);
            return _data;
        }
    }
}
