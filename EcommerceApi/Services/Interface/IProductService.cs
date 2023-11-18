using System;
using EcommerceApi.Dtos;
using EcommerceApi.Models;

namespace EcommerceApi.Services.Interface
{
	public interface IProductService
	{
		Task<IEnumerable<ProductResponseDto>> GetAllAsync();
		Task<ProductDto> GetByIdAsync(int id);
		Task<ProductRequetDto> AddAsync(ProductRequetDto productRequetDto, IFormFile file);
		Task UpdateAsync(ProductDto productDto);
		Task RemoveAsync(int id);
		Task<bool> SkuExistsAsync(string sku);
	}
}

