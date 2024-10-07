using AutoMapper;
using EngAhmed.TaskP.Application.Contracts.IRepostories;
using EngAhmed.TaskP.Application.Dto.DCustomer;
using EngAhmed.TaskP.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EngAhmed.TaskP.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TaskDbContext _db;
        private readonly IMapper _mapper;

        public CustomerRepository(TaskDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var _data = await _db.Customers.FirstOrDefaultAsync(_c => _c.Id==id);
            return _mapper.Map<CustomerDto>(_data);
        }

        public async Task<List<CustomerDto>> GetByNameAsync(string name)
        {
            var _data = await _db.Customers.Where(_c => _c.Name.Contains(name)).ToListAsync();
            var _dto = new List<CustomerDto>();
            return _mapper.Map(_data, _dto);
        }
    }
}
