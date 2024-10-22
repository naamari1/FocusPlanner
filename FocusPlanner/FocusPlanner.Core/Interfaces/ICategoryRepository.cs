using FocusPlanner.Core.Models;
using Task = System.Threading.Tasks.Task;

namespace FocusPlanner.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category task);
        Task DeleteCategoryAsync(int id);
    }
}
