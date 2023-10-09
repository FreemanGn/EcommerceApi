using EcommerceApi.Data;
using EcommerceApi.Models;

namespace EcommerceApi.Services
{
	public class CategoryService : ICategoryService
	{
        private AppDbContext _appDbContext;

		public CategoryService(AppDbContext appDbContext)
		{
            _appDbContext = appDbContext;
		}

        public bool CreateCategory(Category category)
        {
            _appDbContext.Add(category);

            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _appDbContext.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _appDbContext.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _appDbContext.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool UpdateCategory(Category category)
        {
            _appDbContext.Update(category);
            return Save();
        }
        public bool Save()
        {
            var saved = _appDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CategoryExists(int id)
        {
            return _appDbContext.Categories.Any(c => c.Id == id);
        }
    }
}

