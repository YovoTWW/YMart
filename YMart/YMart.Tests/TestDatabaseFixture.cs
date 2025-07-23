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

            // Connect to master to drop the existing test DB (YMartDb)
            DropDatabaseIfExists(connectionString);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            DbContext = new ApplicationDbContext(options);

            // Apply all pending migrations
            DbContext.Database.Migrate();
        }

        private void DropDatabaseIfExists(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var dbName = builder.InitialCatalog;

            // Use master DB to drop existing one
            builder.InitialCatalog = "master";

            using var connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $@"
            IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}')
            BEGIN
                ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE [{dbName}];
            END";
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}

