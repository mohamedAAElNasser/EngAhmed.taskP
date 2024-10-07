using AutoMapper;
using EngAhmed.TaskP.Application.Contracts.IRepostories;
using EngAhmed.TaskP.Application.Dto.DProduct;
using EngAhmed.TaskP.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EngAhmed.TaskP.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TaskDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(TaskDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var _data = await _db.Products.FirstOrDefaultAsync(_c => _c.Id == id);
            return _mapper.Map<ProductDto>(_data);
        }

        public async Task<List<ProductDto>> GetByNameAsync(string name)
        {
            var _data = await _db.Products.Where(_c => _c.Name.Contains(name)).ToListAsync();
            var _dto = new List<ProductDto>();
            return _mapper.Map(_data, _dto);
        }
    }
}
