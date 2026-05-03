using AutoMapper;
using Domain.Entities.OrderEntities;
using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies.MappingProfile
{
    public class OrderProfile:Profile
    {
        public OrderProfile() 
        {
            CreateMap<OrderRequest, Order>(); 
            CreateMap<Order ,OrderResult>();
            CreateMap<shippingDetails,ShippingDetailsDto>();
            CreateMap<ShippingDetailsDto, shippingDetails>();
            CreateMap<OrderItem, OrderitemDto>();
            CreateMap<OrderitemDto, OrderItem>();

        }
    }
}
