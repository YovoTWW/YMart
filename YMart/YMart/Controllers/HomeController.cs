using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using YMart.Data;
using YMart.Data.Models;
using YMart.Models;
using YMart.ViewModels.Brochure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using YMart.ViewModels.Product;
using YMart.ViewModels.HomePage;

namespace YMart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            /*var model = await dbContext.Brochure.Where(b => b.IsActive == true)
                .Select(b => new AddBrochureViewModel()
                {
                    ImageURL = b.ImageURL,
                    ProductNames = b.ProductNames
                }).AsNoTracking().ToListAsync();

            return this.View(model);*/


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

        public IActionResult Privacy()
        {
            return View();
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

        [HttpGet]
        public async Task<IActionResult> AddBrochure()
        {
            ViewData["ProductNamesList"] = dbContext.Products.Where(p => p.IsDeleted == false).Select(p => p.Name).ToList();
            var model = new AddBrochureViewModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBrochure(AddBrochureViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Brochure brochure = new Brochure
            {
                ImageURL = model.ImageURL,
                ProductNames = dbContext.Products.Where(p=>p.IsDeleted==false && model.ProductNames.Contains(p.Name)).Select(pn=>pn.Name).ToList(),
                IsActive = true
            };

            await dbContext.Brochure.AddAsync(brochure);
            await dbContext.SaveChangesAsync();

            return this.RedirectToAction("Index");
        }

        public IActionResult Admin()
        {
            return this.View();
        }

    }
}
