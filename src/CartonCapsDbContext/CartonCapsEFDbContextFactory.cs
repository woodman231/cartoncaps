using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CartonCapsDbContext
{
    public class CartonCapsEFDbContextFactory : IDesignTimeDbContextFactory<CartonCapsEFDbContext>
    {
        public CartonCapsEFDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CartonCapsEFDbContext>();
            var connectionString = configuration.GetConnectionString("CartonCapsDb");
            optionsBuilder.UseSqlServer(connectionString);

            return new CartonCapsEFDbContext(optionsBuilder.Options);
        }
    }
}