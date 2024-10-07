using AutoMapper;
using EngAhmed.TaskP.Application.Dto.DCustomer;
using EngAhmed.TaskP.Application.Dto.DProduct;
using EngAhmed.TaskP.Domain.Enitities.Operations;

namespace EngAhmed.TaskP.Application
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Customer,CustomerDto>().ReverseMap();
            CreateMap<Customer,NewCustomerDto>().ReverseMap();

            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Product,NewProductDto>().ReverseMap();

            
        }

    }
}
