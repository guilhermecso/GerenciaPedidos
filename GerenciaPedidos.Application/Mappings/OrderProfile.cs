using AutoMapper;
using GerenciaPedidos.Application.DTOs;
using GerenciaPedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciaPedidos.Application.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<OrderProduct, OrderProductDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Order.Description))
            .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal));
    }
}
