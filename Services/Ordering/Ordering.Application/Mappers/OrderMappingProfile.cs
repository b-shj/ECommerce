﻿using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappers
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<CheckoutOrderCommand, Order>().ReverseMap();
            CreateMap<CheckoutOrderCommandV2, Order>().ReverseMap();
            CreateMap<UpdateOrderCommand, Order>().ReverseMap();
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
            CreateMap<BasketCheckoutEventV2, CheckoutOrderCommandV2>().ReverseMap();
        }
    }
}
