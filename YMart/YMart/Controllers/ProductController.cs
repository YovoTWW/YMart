using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
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
        public async Task<IActionResult> Index(string CategoryFilter, List<string> productNames)
        {
            ViewData["CategoryFilter"] = CategoryFilter;

            var query = dbContext.Products.Where(p => p.IsDeleted == false);

            if (productNames != null && productNames.Any())
            {
                query = query.Where(p => productNames.Contains(p.Name));
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

                if (!string.IsNullOrEmpty(CategoryFilter) && CategoryFilter != "All")
                {
                    model = model.Where(p => p.Category == CategoryFilter).ToList();          
                }

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
        public async Task<IActionResult> Details(Guid id/*, string returnController, string returnAction*/)
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
               /* PreviousPageAction = returnAction ?? "Index",
                PreviousPageController = returnController ?? "Home"*/
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

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            var model = await dbContext.Products.Where(p => p.IsDeleted == false).Where(p => p.Carts.Any(pc => pc.ClientId == currentUserId))
                .Select(p => new BasicProductViewModel()
                {
                    Id = p.Id,
                    ImageURL = p.ImageURL,
                    Name = p.Name,
                    Price = p.Price,
                    DiscountedPrice = p.DiscountedPrice,
                    Quantity = p.Quantity,
                }).AsNoTracking().ToListAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid id,string returnController, string returnAction)
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;
            Product? entity = await dbContext.Products.Where(p => p.Id == id).Include(p => p.Carts).FirstOrDefaultAsync();

            if (entity == null || entity.IsDeleted)
            {
                throw new ArgumentException("Invalid id");
            }

            if (entity.Carts.Any(pc => pc.ClientId == currentUserId))
            {
                TempData["ShowPopup"] = true;
                if (!string.IsNullOrEmpty(returnAction) && !string.IsNullOrEmpty(returnController))
                {
                    return this.RedirectToAction(returnAction, returnController);
                }

                return this.RedirectToAction("Index");
          
            }

            entity.Carts.Add(new Cart()
            {
                ClientId = currentUserId,
                ProductId = entity.Id
            });

            await dbContext.SaveChangesAsync();

            return this.RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            Product? entity = await dbContext.Products.Where(p => p.Id == id).Include(p => p.Carts).FirstOrDefaultAsync();

            Cart? cart = entity.Carts.FirstOrDefault(pc => pc.ClientId == currentUserId);

            if (entity == null || entity.IsDeleted)
            {
                throw new ArgumentException("Invalid id");
            }

            if (cart != null)
            {
                entity.Carts.Remove(cart);

                await dbContext.SaveChangesAsync();
            }

            return this.RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPurchase(List<Guid> ProductIds, List<int> Quantities)
        {
            string userId = GetCurrentUserId() ?? string.Empty;
            if (ProductIds.Count != Quantities.Count)
            {
                TempData["Error"] = "Invalid input.";
                return RedirectToAction("Cart");
            }

            var products = await dbContext.Products
                .Where(p => ProductIds.Contains(p.Id))
                .ToListAsync();

            for (int i = 0; i < ProductIds.Count; i++)
            {
                var productId = ProductIds[i];
                var quantityRequested = Quantities[i];
                var product = products.FirstOrDefault(p => p.Id == productId);

                if (product == null || product.IsDeleted)
                {
                    TempData["Error"] = "Product not found.";
                    return RedirectToAction("Cart");
                }

                if (quantityRequested > product.Quantity)
                {
                    TempData["Error"] = $"Not enough stock for {product.Name}.";
                    return RedirectToAction("Cart");
                }
            }

            // Proceed with "purchase" logic
            for (int i = 0; i < ProductIds.Count; i++)
            {
                var productId = ProductIds[i];
                var quantity = Quantities[i];
                var product = products.First(p => p.Id == productId);

                product.Quantity -= quantity;

               /* dbContext.Orders.Add(new Order
                {
                    Id = Guid.NewGuid(),
                    ClientId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    OrderDate = DateTime.UtcNow
                });*/

                // Optionally: Remove from Cart if your logic includes that
               // var cartItem = await dbContext.Products.Where(p => p.IsDeleted == false).Where(p => p.Carts.Any(pc => pc.ClientId == userId)).ToListAsync();
                //if (cartItem != null)
               // {
                //    dbContext.Carts.Remove(cartItem);
               // }
            }

            await dbContext.SaveChangesAsync();

            TempData["Success"] = "Purchase successful!";
            //return RedirectToAction("OrderConfirmation");
            return RedirectToAction("Cart");
        }


        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
