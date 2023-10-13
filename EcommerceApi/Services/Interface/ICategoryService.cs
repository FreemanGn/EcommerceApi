using EcommerceApi.Dtos;
using EcommerceApi.Models;

namespace EcommerceApi.Services
{
	public interface ICategoryService
	{
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> AddAsync(CategoryDto categoryDto);
        Task UpdateAsync(CategoryDto categoryDto);
        Task RemoveAsync(int id);
        Task<bool> CategoryExists(int id);
    }
}

