using System;
using AutoMapper;
using EcommerceApi.Dtos;
using EcommerceApi.Models;

namespace EcommerceApi.Profiles
{
	public class CategoryProfile : Profile
    {
		public CategoryProfile()
		{
			CreateMap<Category, CategoryDto>().ReverseMap();
		}
	}
}

