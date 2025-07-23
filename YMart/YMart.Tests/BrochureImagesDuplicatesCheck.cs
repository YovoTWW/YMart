using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YMart.Data;

namespace YMart.Tests
{
    public class BrochureImagesDuplicatesCheck : IClassFixture<TestDatabaseFixture>
    {
        private readonly ApplicationDbContext _dbContext;

      
        public BrochureImagesDuplicatesCheck(TestDatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
        }

        [Fact]
        public async Task NoDuplicateBrochureImages()
        {
            var brochures = await _dbContext.Brochure.ToListAsync();

            var duplicateImages = brochures
                .GroupBy(b => b.ImageURL)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            Assert.True(!duplicateImages.Any(), $"Duplicate brochure images found: {string.Join(", ", duplicateImages)}");
        }
    }
}
