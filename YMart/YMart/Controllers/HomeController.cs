using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using YMart.Data;
using YMart.Models;
using YMart.ViewModels.Brochure;
using YMart.ViewModels.Product;
using YMart.ViewModels.HomePage;

namespace YMart.Controllers
{
    public class HomeController : Controller
    {      
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context)
        {          
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Brochures = await dbContext.Brochure.Where(b => b.IsActive == true)
                .Select(b => new AddBrochureViewModel()
                {
                    ImageURL = b.ImageURL,
                    ProductNames = b.ProductNames
                }).AsNoTracking().ToListAsync();

            var Offers = await dbContext.Products.Where(p => p.IsDeleted == false && p.IsOnSale && p.DiscountPercentage.HasValue)
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

            var model = new HomePageViewModel()
            {
                Offers = Offers,
                Brochures = Brochures
            };

            return this.View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NotSignedIn()
        {
            return this.View();
        }

        public IActionResult Admin()
        {
            return this.View();
        }

    }
}
