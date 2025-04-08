using AutoMapper;
using e_order.Domain.Models;
using e_order.Domain.ViewModels;
using E_Order.Domain.Models.Dto;

namespace e_order.Common
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Client, ClientViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
            CreateMap<Person, PersonViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<OrderItem, OrderToClientDto>().ReverseMap();
            CreateMap<OrderItemDto, OrderToClientDto>().ReverseMap();
        }
    }
}