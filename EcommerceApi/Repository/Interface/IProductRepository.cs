using System;
using EcommerceApi.Models;

namespace EcommerceApi.Repository.Interface
{
	public interface IProductRepository
	{
		Task<Product> GetByIdAsync(int id);
		Task<IEnumerable<Product>> GetAllAsync();
		Task AddAsync(Product entity);
		Task UpdateAsync(Product entity);
		Task RemoveAsync(Product entity);

        Task<bool> SkuExistsAsync(string sku);
    }
}

