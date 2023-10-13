using EcommerceApi.Data;
using EcommerceApi.Models;
using EcommerceApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
        private readonly AppDbContext _dbContext;
        public CategoryRepository(AppDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        public async Task AddAsync(Category entity)
        {
            await _dbContext.Categories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CategoryExist(int id)
        {
            return await _dbContext.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
        }

        public async Task RemoveAsync(Category entity)
        {
            _dbContext.Categories.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            _dbContext.Categories.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

