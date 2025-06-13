using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityDTos;
using Shared.DataTransferObjects.OrderDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    internal class OrderProfile :Profile
    {

        public OrderProfile()
        {
            CreateMap<AddressDTo, OrderAddress>().ReverseMap();

            CreateMap<Order , OrderToReturnDTo>().ForMember(d=>d.DeliveryMethod , o=>o.MapFrom(s=>s.DeliveryMethod.ShortName));


            CreateMap<OrderItem, OrderItemDTo>().ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());


            CreateMap<DeliveryMethod, DeliveryMethodDTo>();


        }
    }
}
