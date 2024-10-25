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
                    StartDate = DateTime.Now.AddDays(1).AddHours(9), // Start morgen om 9:00 AM
                    DueDate = DateTime.Now.AddDays(3).AddHours(17), // Due over drie dagen om 5:00 PM
                    FinishDate = DateTime.Now.AddDays(3).AddHours(16), // Finish op dezelfde dag als due, maar een uur eerder
                    IsCompleted = false,
                    CategoryId = 1,
                    Priority = Core.Enum.Priority.High
                },
                new Core.Models.Task
                {
                    Id = 2,
                    Title = "Gym Workout",
                    Description = "Complete full body workout",
                    StartDate = DateTime.Now.AddHours(2), // Start vandaag over twee uur
                    DueDate = DateTime.Now.AddDays(1).AddHours(12), // Due morgen om 12:00 PM
                    FinishDate = DateTime.Now.AddDays(1).AddHours(13), // Finish een uur na de due date
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
