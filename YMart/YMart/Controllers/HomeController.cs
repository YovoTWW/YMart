using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using YMart.Data;
using YMart.Data.Models;
using YMart.Models;
using YMart.ViewModels.Brochure;

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
            var model = await dbContext.Brochure.Where(b=>b.IsActive == true)
                .Select(b=>b.ImageURL).AsNoTracking().ToListAsync();

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
                Products = model.Products,
                IsActive = true
            };

            await dbContext.Brochure.AddAsync(brochure);
            await dbContext.SaveChangesAsync();

            return this.RedirectToAction("Index");
        }
    }
}
