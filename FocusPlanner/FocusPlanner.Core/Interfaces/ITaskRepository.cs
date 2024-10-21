namespace FocusPlanner.Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<Core.Models.Task> GetTaskByIdAsync(int id);
        Task<IEnumerable<Core.Models.Task>> GetAllTasksAsync();
        Task AddTaskAsync(Core.Models.Task task);
        Task UpdateTaskAsync(Core.Models.Task task);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<Core.Models.Task>> GetTasksByCategoriesSearchTermCompletionStatusAndDueDateAsync(List<int> categoryIds, string searchTerm, bool showCompletedTasks, DateTime? dueDateFilter);

    }
}
