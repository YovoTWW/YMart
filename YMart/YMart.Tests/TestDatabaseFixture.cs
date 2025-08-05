using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YMart.Data;

namespace YMart.Tests
{
    public class TestDatabaseFixture : IDisposable
    {
        public ApplicationDbContext DbContext { get; private set; }

        public TestDatabaseFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("AzureConnection");

            Console.WriteLine($"Using connection string: {connectionString}");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            DbContext = new ApplicationDbContext(options);

            Console.WriteLine("Applying migrations (Azure)...");
            DbContext.Database.Migrate();
            Console.WriteLine("Migrations applied.");
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}


