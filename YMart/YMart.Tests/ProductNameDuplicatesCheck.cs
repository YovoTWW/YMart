using YMart.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace YMart.Tests
{
    [Collection("Database collection")]
    public class ProductNameDuplicatesCheck : IClassFixture<TestDatabaseFixture>
    {
        private readonly ApplicationDbContext _dbContext;

       
        public ProductNameDuplicatesCheck(TestDatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
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