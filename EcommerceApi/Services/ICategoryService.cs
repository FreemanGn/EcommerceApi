using EcommerceApi.Models;

namespace EcommerceApi.Services
{
	public interface ICategoryService
	{
		ICollection<Category> GetCategories();
		Category GetCategory(int id);
		bool CreateCategory(Category category);
		bool UpdateCategory(Category category);
		bool DeleteCategory(Category category);
        bool Save();
        bool CategoryExists(int id);
    }
}

