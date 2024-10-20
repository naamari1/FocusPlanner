using FocusPlanner.Core.Interfaces;
using FocusPlanner.Core.Models;
using FocusPlanner.Infastructure.Data_Access;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;


namespace FocusPlanner.Infastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TaskDbContext _taskDbContext;

        public CategoryRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _taskDbContext.Categories.AddAsync(category);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _taskDbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _taskDbContext.Categories.Remove(category);
                await _taskDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _taskDbContext.Categories
                .Include(t => t.Tasks)
                .ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var categoriy = await _taskDbContext.Categories
                .Include(t => t.Tasks)
                .ThenInclude(r => r.Reminders)
                .FirstOrDefaultAsync(c => c.Id == id);

            return categoriy;
        }

        public async Task UpdateCategoryAsync(Category task)
        {
            _taskDbContext.Categories.Update(task);
            await _taskDbContext.SaveChangesAsync();
        }
    }
}
