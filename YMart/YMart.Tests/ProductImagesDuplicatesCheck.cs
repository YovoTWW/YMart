using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YMart.Data;

namespace YMart.Tests
{
    public class ProductImagesDuplicatesCheck
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductImagesDuplicatesCheck()
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
        public async Task NoDuplicateProductImages()
        {
            var products = await _dbContext.Products.ToListAsync();

            var duplicateImages = products
                .GroupBy(p => p.ImageURL)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            Assert.True(!duplicateImages.Any(), $"Duplicate product images found: {string.Join(", ", duplicateImages)}");
        }
    }
}
