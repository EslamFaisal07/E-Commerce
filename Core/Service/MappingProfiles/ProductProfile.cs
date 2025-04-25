using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTo>()
                .ForMember(p => p.BrandName, option => option.MapFrom(src => src.ProductBrand.Name))
                .ForMember(p => p.TypeName, option => option.MapFrom(src => src.ProductType.Name))
                .ForMember(p=>p.PictureUrl , option =>option.MapFrom<PictureUrlResolver>());



            CreateMap<ProductBrand, BrandDTo>();
            CreateMap<ProductType, TypeDTo>();




        }
    }
}
