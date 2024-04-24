﻿using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
	public class MappingProfiles : Profile
	{

		public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand , O => O.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category , O => O.MapFrom(s => s.Category.Name))
                .ForMember(P => P.PictureUrl , O => O.MapFrom<ProductsPictureUrlResolver>()); //Resolver 

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

                
		}
    }
}
