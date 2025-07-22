using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YMart.Data;

namespace YMart.Tests
{
    public class BrochureImagesDuplicatesCheck : IClassFixture<TestDatabaseFixture>
    {
        private readonly ApplicationDbContext _dbContext;

        /* public BrochureImagesDuplicatesCheck()
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
         }*/

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
