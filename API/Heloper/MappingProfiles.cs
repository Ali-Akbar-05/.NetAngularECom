using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Heloper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(des => des.ProductBrand, o => o.MapFrom(src => src.ProductBrand.Name))
            .ForMember(des => des.ProductType, o => o.MapFrom(src => src.ProductType.Name))
            .ForMember(des => des.ProductUrl, o => o.MapFrom<ProductUrlResolver>())
            ;
        }
    }
}