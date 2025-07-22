using YMart.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace YMart.Tests
{
    public class ProductNameDuplicatesCheck
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductNameDuplicatesCheck()
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                 .Build();

            var connectionString = config.GetConnectionString("SqlServer");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            _dbContext = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task NoDuplicateProductNames()
        {
            var products = await _dbContext.Products.ToListAsync();

            var duplicateNames = products
                .GroupBy(p => p.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            //Assert.Empty(duplicateNames);
            Assert.True(!duplicateNames.Any(), $"Duplicate product names found: {string.Join(", ", duplicateNames)}");
        }

    }
}