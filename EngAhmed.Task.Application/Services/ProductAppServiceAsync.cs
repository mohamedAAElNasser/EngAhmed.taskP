using AutoMapper;
using EngAhmed.TaskP.Application.Contracts.IAppService;
using EngAhmed.TaskP.Application.Contracts.IPersistence;
using EngAhmed.TaskP.Application.Contracts.IRepostories;
using EngAhmed.TaskP.Application.Dto.DProduct;
using EngAhmed.TaskP.Domain.Enitities.Operations;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EngAhmed.TaskP.Application.Services
{
    public class ProductAppServiceAsync : IProductAppServiceAsync
    {
        private readonly IBaseRepository<Product> _rep;
        private readonly IMapper _mapper;
        private readonly IProductRepository _prodRep;
        public ProductAppServiceAsync(IBaseRepository<Product> rep, IMapper mapper, IProductRepository prodRep)
        {
            _rep = rep;
            _mapper = mapper;
            _prodRep = prodRep;
        }
        public async Task<ProductDto> CreateProductAsync(NewProductDto obj)
        {
            var _productEntity = _mapper.Map<Product>(obj);
            var _productCreated = await _rep.CreateAsync(_productEntity);
            return _mapper.Map<ProductDto>(_productCreated);
        }

        public async Task<ProductDto> DeleteProductAsync(int id)
        {
            var _data = await _prodRep.GetByIdAsync(id);
            if (_data == null)
                return new ProductDto();
            var _productTarget = _mapper.Map<Product>(_data);
            var _productDeleted = await _rep.DeleteAsync(_productTarget);
            return _mapper.Map<ProductDto>(_productDeleted);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var _data = await _rep.GetAllAsync();
            var _dto = new List<ProductDto>();
            return _mapper.Map(_data, _dto);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var _productTarget = await _prodRep.GetByIdAsync(id);
            return _productTarget;
        }

        public async Task<List<ProductDto>> GetProductByNameAsync(string name)
        {
            var _productList = await _prodRep.GetByNameAsync(name);
            return _productList;
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto obj)
        {
            var _productTarget = await _prodRep.GetByIdAsync(obj.Id);
            if (_productTarget != null)
            {
                var _productEntity = _mapper.Map<Product>(obj);
                var _productUpdated = await _rep.UpdateAsync(_productEntity);
                return _mapper.Map<ProductDto>(_productUpdated);
            }
            return new ProductDto();
        }
        public byte[] GenerateProductReport(List<ProductDto> products)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var _document = Document.Create(_container =>
            {
                _container.Page(_page =>
                {
                    _page.Size(PageSizes.A4);
                    _page.Margin(2, Unit.Centimetre);
                    _page.Header().Text("Product Report").FontSize(20).SemiBold().AlignCenter();

                    _page.Content().Table(_table =>
                    {
                        _table.ColumnsDefinition(_columns =>
                        {
                            _columns.ConstantColumn(75); 
                            _columns.RelativeColumn();   
                            _columns.RelativeColumn();  
                        });

                        _table.Header(_header =>
                        {
                            _header.Cell().Element(CellStyle).Text("ID");
                            _header.Cell().Element(CellStyle).Text("Name");
                            _header.Cell().Element(CellStyle).Text("Price");
                        });

                        foreach (var _product in products)
                        {
                            _table.Cell().Element(CellStyle).Text(_product.Id.ToString());
                            _table.Cell().Element(CellStyle).Text(_product.Name);
                            _table.Cell().Element(CellStyle).Text(_product.Price.ToString("C"));
                        }

                        static IContainer CellStyle(IContainer _container) =>
                            _container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });
                    _page.Footer()
    .AlignCenter()
    .Text($"Total Number of Branches: {products.Count}")
    .FontSize(12);
                });
            });

       
            using (var _stream = new MemoryStream())
            {
                _document.GeneratePdf(_stream);  
                return _stream.ToArray(); 
            }
        }

    }
}
