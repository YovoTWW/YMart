using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YMart.Data;
using YMart.Data.Models;
using YMart.ViewModels.Product;
using YMart.ViewModels.Order;
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
        public async Task<IActionResult> Index(List<string> CategoryFilters, List<string> productNames,string searchTextName)
        {
            ViewData["CategoryFilters"] = CategoryFilters;
            ViewData["SearchTextName"] = searchTextName;

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
                //query = query.Where(e => e.Name.Contains(searchTextName, StringComparison.OrdinalIgnoreCase));
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
                DiscountPercentage = model.DiscountPercentage,
                IsOnSale = model.IsOnSale,
                ImageURL = model.ImageURL,
                Category = model.Category,
                Quantity = model.Quantity
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await dbContext.Products.Where(p => p.Id == id).Where(p => p.IsDeleted == false).AsNoTracking().Select(p => new DetailedProductViewModel
            {
                Id = p.Id,
                Description = p.Description,
                Category = p.Category,
                ImageURL = p.ImageURL,                
                Name = p.Name,               
                Price = p.Price,
                DiscountedPrice = p.DiscountedPrice,
                DiscountPercentage = p.DiscountPercentage,
                Quantity= p.Quantity,              
            }).FirstOrDefaultAsync();


            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await dbContext.Products.Where(p => p.Id == id).Where(p => p.IsDeleted == false).AsNoTracking().Select(p => new EditProductViewModel
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageURL = p.ImageURL,
                Category = p.Category,
                Quantity = p.Quantity,
                IsOnSale = p.IsOnSale,
                DiscountPercentage = p.DiscountPercentage,
            }).FirstOrDefaultAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model, Guid id)
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

            if (model.DiscountPercentage < 0 || model.DiscountPercentage > 100)
            {
                ModelState.AddModelError(nameof(model.DiscountPercentage), $"Discount Percentage must be between {0}% and {100}%");

                return this.View(model);
            }

            Product? entity = await dbContext.Products.FindAsync(id);

            if (entity == null || entity.IsDeleted)
            {
                throw new ArgumentException("Invalid id");
            }

            //entity.Id = id;
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.ImageURL = model.ImageURL;            
            entity.Category = model.Category;
            entity.Quantity = model.Quantity;
            entity.IsOnSale = model.IsOnSale;
            entity.DiscountPercentage = model.DiscountPercentage;

            await this.dbContext.SaveChangesAsync();

            //return RedirectToAction("Index");
            return RedirectToAction("Details", new { id = id });
        }


        [HttpGet]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var model = await dbContext.Products.Where(p => p.Id == id).Where(p => p.IsDeleted == false).AsNoTracking().Select(p => new DeleteProductViewModel
            {
                Id = p.Id,
                Name = p.Name
            }).FirstOrDefaultAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(DeleteProductViewModel model)
        {
            Product? product = await dbContext.Products.Where(p => p.Id == model.Id).Where(p => p.IsDeleted == false).FirstOrDefaultAsync();

            if (product != null)
            {

                product.IsDeleted = true;

                await dbContext.SaveChangesAsync();
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            var model = await dbContext.Products.Where(p => p.Id == id).Where(p => p.IsDeleted == false).AsNoTracking().Select(p => new DeleteProductViewModel
            {
                Id = p.Id,
                Name = p.Name
            }).FirstOrDefaultAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> HardDelete(DeleteProductViewModel model)
        {
            Product? product = await dbContext.Products.Where(p => p.Id == model.Id).FirstOrDefaultAsync();

            if (product != null)
            {

                dbContext.Products.Remove(product);

                await dbContext.SaveChangesAsync();
            }

            return this.RedirectToAction("Index");
        }
        
    }
}
