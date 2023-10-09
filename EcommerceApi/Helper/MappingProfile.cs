using System;
using AutoMapper;
using EcommerceApi.Dtos;
using EcommerceApi.Models;

namespace EcommerceApi.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Category, CategoryDto>();
			CreateMap<CategoryDto, Category>();
		}
	}
}

