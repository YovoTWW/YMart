using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YMart.Data;

namespace YMart.Tests
{
    [Collection("Database collection")]
    public class ProductImagesDuplicatesCheck : IClassFixture<TestDatabaseFixture>
    {
        private readonly ApplicationDbContext _dbContext;

       
        public ProductImagesDuplicatesCheck(TestDatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
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
