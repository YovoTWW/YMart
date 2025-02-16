using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using YMart.Data;
using YMart.Data.Models;
using YMart.ViewModels.Product;
using static YMart.Constants.DataConstants;

namespace YMart.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProductController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var model = await dbContext.Products.Where(p => p.IsDeleted == false)
                .Select(p => new BasicProductViewModel()
                {
                    ImageURL = p.ImageURL,
                    Name = p.Name,
                    Price = p.Price,                    
                }).AsNoTracking().ToListAsync();

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddProductViewModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!this.ModelState.IsValid)
            {               
                return this.View(model);
            }

                  
            if (model.Price < MinPrice || model.Price > MaxPrice)
            {
                ModelState.AddModelError(nameof(model.Price), $"Price must be between {MinPrice}€ and {MaxPrice}€");             
                return this.View(model);
            }

            Product product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageURL = model.ImageURL,
                Category = model.Category,
                Quantity = model.Quantity
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return this.RedirectToAction("Index");
        }
    }
}
