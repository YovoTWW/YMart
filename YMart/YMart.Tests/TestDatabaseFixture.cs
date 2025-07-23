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

            var connectionString = config.GetConnectionString("DockerSqlServer");

            Console.WriteLine($"Using connection string: {connectionString}");

            // Drop the existing test DB if it exists
            DropDatabaseIfExists(connectionString);

            // Wait a bit to make sure DB is fully dropped
            Thread.Sleep(2000); // optional, useful in Docker

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            DbContext = new ApplicationDbContext(options);

            Console.WriteLine("Applying migrations...");
            DbContext.Database.Migrate();
            Console.WriteLine("Migrations applied.");
        }

        private void DropDatabaseIfExists(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var dbName = builder.InitialCatalog;

            // Connect to master
            builder.InitialCatalog = "master";

            try
            {
                using var connection = new SqlConnection(builder.ConnectionString);
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = $@"
                    IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}')
                    BEGIN
                        ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        DROP DATABASE [{dbName}];
                    END";

                Console.WriteLine($"Attempting to drop database: {dbName}");
                command.ExecuteNonQuery();
                Console.WriteLine($"Database '{dbName}' dropped successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error dropping database '{dbName}': {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}


