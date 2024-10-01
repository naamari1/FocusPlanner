using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FocusPlanner.Infastructure.Data_Access
{
    public class TaskDbContextFactory : IDesignTimeDbContextFactory<TaskDbContext>
    {
        public TaskDbContext CreateDbContext(string[] args)
        {
            // Bouw de configuratie
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Haal de connectiestring op uit appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configureer de DbContext-opties
            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TaskDbContext(optionsBuilder.Options);
        }
    }
}
