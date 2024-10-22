using FocusPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusPlanner.Infastructure.Data_Access
{
    public class TaskDbContext : DbContext
    {

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {

        }

        public DbSet<Core.Models.Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Data seeding voor Category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Work" },
                new Category { Id = 2, Name = "Personal" },
                new Category { Id = 3, Name = "Fitness" }
            );

            // Data seeding voor Task
            modelBuilder.Entity<Core.Models.Task>().HasData(
                new Core.Models.Task
                {
                    Id = 1,
                    Title = "Complete Project",
                    Description = "Finish the FocusPlanner project",
                    DueDate = DateTime.Now.AddDays(3),
                    IsCompleted = false,
                    CategoryId = 1,
                    Priority = Core.Enum.Priority.High
                },
                new Core.Models.Task
                {
                    Id = 2,
                    Title = "Gym Workout",
                    Description = "Complete full body workout",
                    DueDate = DateTime.Now.AddDays(1),
                    IsCompleted = false,
                    CategoryId = 3,
                    Priority = Core.Enum.Priority.Medium

                }
            );

            // Data seeding voor Reminder
            modelBuilder.Entity<Reminder>().HasData(
                new Reminder
                {
                    Id = 1,
                    ReminderTime = DateTime.Now.AddDays(3).AddHours(-1),
                    TaskId = 1
                },
                new Reminder
                {
                    Id = 2,
                    ReminderTime = DateTime.Now.AddDays(1).AddHours(-2),
                    TaskId = 2
                }
            );
        }
    }


}
