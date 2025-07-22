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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            DbContext = new ApplicationDbContext(options);

            // Apply any pending migrations
            if (!DbContext.Database.CanConnect())
            {
                DbContext.Database.Migrate();
            }
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}

