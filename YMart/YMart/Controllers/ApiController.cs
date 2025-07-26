using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using YMart.Data;
using YMart.ViewModels.Brochure;
using YMart.ViewModels.Order;
using YMart.ViewModels.Product;

namespace YMart.Controllers
{
    using static YMart.Constants.DataConstants;
    public class ApiController : Controller
    {

        private readonly ApplicationDbContext dbContext;

        public ApiController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }


        [HttpGet]
        [Route("api/products")]
        public async Task<IActionResult> GetProducts(
        [FromQuery] List<string> CategoryFilters,
        [FromQuery] List<string> productNames,
        [FromQuery] string searchTextName)
        {
            var query = dbContext.Products.Where(p => p.IsDeleted == false);

            if (productNames != null && productNames.Any())
            {
                query = query.Where(p => productNames.Contains(p.Name));
            }

            if (CategoryFilters != null && CategoryFilters.Any())
            {
                query = query.Where(p => CategoryFilters.Contains(p.Category));
            }

            if (!string.IsNullOrWhiteSpace(searchTextName))
            {
                var lowerSearch = searchTextName.ToLower();
                query = query.Where(e => e.Name.ToLower().Contains(lowerSearch));
            }

            var model = await query
                .Select(p => new BasicProductViewModel()
                {
                    Id = p.Id,
                    ImageURL = p.ImageURL,
                    Name = p.Name,
                    Price = p.Price,
                    DiscountedPrice = p.DiscountedPrice,
                    DiscountPercentage = p.DiscountPercentage,
                    Quantity = p.Quantity,
                    Category = p.Category
                }).AsNoTracking().ToListAsync();

            return Ok(model); // Returns JSON
        }

        [HttpGet]
        [Route("api/brochures")]
        public async Task<IActionResult> GetBrochures()
        {
            var model = await dbContext.Brochure.Select(b => new BasicBrochureViewModel()
            {
                Id = b.Id,
                ImageURL = b.ImageURL,
                ProductNames = b.ProductNames,
                IsActive = b.IsActive
            }).ToListAsync();

            return Ok(model);
        }

        [HttpGet]
        [Route("api/orders")]
        public async Task<IActionResult> GetOrders()
        {
            var model = await dbContext.Orders.AsNoTracking()
                    .Select(o => new OrderViewModel()
                    {
                        Id = o.Id,
                        ClientEmail = o.ClientEmail,
                        ItemPrices = o.ItemPrices,
                        ItemNames = o.ItemNames,
                        ItemQuantities = o.ItemQuantities,
                        TotalPrice = o.TotalPrice,
                        OrderTime = o.OrderTime.ToString(DateFormat)
                    }).ToListAsync();

            return Ok(model);
        }
    }
}
