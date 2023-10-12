using System;
using AutoMapper;
using EcommerceApi.Dtos;
using EcommerceApi.Models;

namespace EcommerceApi.Profiles
{
	public class ProductProfile :Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductDto>().ReverseMap();
		}
	}
}

