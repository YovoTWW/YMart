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

            if (entity == null || entity.IsDeleted)
            {
                throw new ArgumentException("Invalid id");
            }

            Cart? cart = entity.Carts.FirstOrDefault(pc => pc.ClientId == currentUserId);
 

            if (cart != null)
            {
                entity.Carts.Remove(cart);

                await dbContext.SaveChangesAsync();
            }

            return this.RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> OrderConfirmation(List<Guid> ProductIds, List<int> Quantities)
        {
            string userId = GetCurrentUserId() ?? string.Empty;        

            if(ProductIds == null || Quantities == null || ProductIds.Count == 0 || Quantities.Count == 0)
            {
                ViewBag.ErrorMessage = "Cannot make a purchase with an empty cart.";
                return View();
            }

            if (ProductIds.Count != Quantities.Count)
            {
                ViewBag.ErrorMessage = "Invalid input.";
                return View();
            }

            var products = await dbContext.Products
                .Where(p => ProductIds.Contains(p.Id))
                .ToListAsync();

            List<decimal> ItemPrices = new List<decimal>();

            List<string> ItemNames = new List<string>();

            List<int> ItemQuantities = new List<int>();

            for (int i = 0; i < ProductIds.Count; i++)
            {
                var productId = ProductIds[i];
                var quantityRequested = Quantities[i];
                var product = products.FirstOrDefault(p => p.Id == productId);

                if (product == null || product.IsDeleted)
                {
                    ViewBag.ErrorMessage = "Product not found.";
                    return View();
                }

                if (quantityRequested > product.Quantity)
                {
                    ViewBag.ErrorMessage = $"Not enough stock for {product.Name}.";
                    return View();
                }
            }

            decimal totalPrice = 0;

            for (int i = 0; i < ProductIds.Count; i++)
            {
                var productId = ProductIds[i];
                var quantity = Quantities[i];
                var product = products.First(p => p.Id == productId);


                ItemPrices.Add(product.DiscountedPrice);
                ItemNames.Add(product.Name);
                ItemQuantities.Add(quantity);
                totalPrice += product.DiscountedPrice * quantity;

                product.Quantity -= quantity;

            }

            Order order = new Order
            {
                ItemPrices = ItemPrices,
                ItemNames = ItemNames,
                ItemQuantities = ItemQuantities,
                TotalPrice = totalPrice,
                ClientEmail = GetCurrentUserEmail() ?? string.Empty,
                OrderTime = DateTime.Now
            };


            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            ViewBag.SuccessMessage = "Purchase successful!";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {
            string userEmail = GetCurrentUserEmail() ?? string.Empty;

            var query = dbContext.Orders.Where(o => o.ClientEmail == userEmail);
            

            var model = await query
                    .Select(o => new MiniOrderViewModel()
                    {
                        Id = o.Id,
                        TotalPrice = o.TotalPrice,
                        OrderTime = o.OrderTime.ToString(DateFormat)
                    }).AsNoTracking().ToListAsync();

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(Guid id)
        {
            var model = await dbContext.Orders.Where(o => o.Id == id).AsNoTracking()
                    .Select(o => new OrderViewModel()
                    {
                        Id = o.Id,
                        ClientEmail = o.ClientEmail,
                        ItemPrices = o.ItemPrices,
                        ItemNames = o.ItemNames,
                        ItemQuantities = o.ItemQuantities,
                        TotalPrice = o.TotalPrice,
                        OrderTime = o.OrderTime.ToString(DateFormat)
                    }).FirstOrDefaultAsync();

            return this.View(model);
        }


        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private string? GetCurrentUserEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }
    }
}
