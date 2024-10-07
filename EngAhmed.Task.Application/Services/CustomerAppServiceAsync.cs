using AutoMapper;
using EngAhmed.TaskP.Application.Contracts.IAppService;
using EngAhmed.TaskP.Application.Contracts.IPersistence;
using EngAhmed.TaskP.Application.Contracts.IRepostories;
using EngAhmed.TaskP.Application.Dto.DCustomer;
using EngAhmed.TaskP.Domain.Enitities.Operations;

namespace EngAhmed.TaskP.Application.Services
{
    public class CustomerAppServiceAsync : ICustomerAppServiceAsync
    {
        private readonly IBaseRepository<Customer> _rep;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _custRep;
        public CustomerAppServiceAsync(IBaseRepository<Customer> rep, IMapper mapper, ICustomerRepository custRep)
        {
            _rep = rep;
            _mapper = mapper;
            _custRep = custRep;
        }

        public async Task<CustomerDto> CreateCustomerAsync(NewCustomerDto obj)
        {
            var _customerEntity = _mapper.Map<Customer>(obj);
            var _customerCreated = await _rep.CreateAsync(_customerEntity);
            return _mapper.Map<CustomerDto>(_customerCreated);
        }

        public async Task<CustomerDto> DeleteCustomerAsync(int id)
        {
            var _data = await _custRep.GetByIdAsync(id);
            if (_data == null) 
                return new CustomerDto();
            var _customerTarget = _mapper.Map<Customer>(_data);
            var _customerDeleted = await _rep.DeleteAsync(_customerTarget);
            return _mapper.Map<CustomerDto>(_customerDeleted);
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            var _data = await _rep.GetAllAsync(); 
            var _dto = new List<CustomerDto>();
            return _mapper.Map(_data, _dto);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var _customerTarget = await _custRep.GetByIdAsync(id);
            return _customerTarget;
        }

        public async Task<List<CustomerDto>> GetCustomersByNameAsync(string name)
        {
            var _customerList = await _custRep.GetByNameAsync(name);
            return _customerList;
        }

        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto obj)
        {
            var _customerTarget = await _custRep.GetByIdAsync(obj.Id);
            if (_customerTarget != null)
            {
                var _customerEntity = _mapper.Map<Customer>(obj);
                var _customerUpdated = await _rep.UpdateAsync(_customerEntity);
                return _mapper.Map<CustomerDto>(_customerUpdated);
            }
            return new CustomerDto();
        }
    }
}
