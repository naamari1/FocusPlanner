﻿using FocusPlanner.Core.Interfaces;
using FocusPlanner.Infastructure.Data_Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FocusPlanner.Infastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;
        private readonly IServiceProvider _serviceProvider;


        public TaskRepository(TaskDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async System.Threading.Tasks.Task AddTaskAsync(Core.Models.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Core.Models.Task>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks
          .Include(t => t.Category)
          .Include(t => t.Reminders)
          .ToListAsync();

            return tasks;
        }

        public async Task<Core.Models.Task> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks
                            .Include(t => t.Category)
                            .Include(t => t.Reminders)
                            .FirstOrDefaultAsync(t => t.Id == id);

            return task;
        }

        public async Task<IEnumerable<Core.Models.Task>> GetTasksByCategoriesAsync(IEnumerable<int> categoryIds)
        {
            // Create a new scope for the DbContext
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
                var tasks = await context.Tasks
                    .Where(t => categoryIds.Contains(t.CategoryId))
                    .Include(t => t.Category)
                    .ToListAsync();
                return tasks;
            }
        }


        public async System.Threading.Tasks.Task UpdateTaskAsync(Core.Models.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}