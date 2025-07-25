using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YMart.Data;
using YMart.Data.Models;
using YMart.ViewModels.Product;

namespace YMart.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CartController(ApplicationDbContext context)
        {
            dbContext = context;
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
        public async Task<IActionResult> AddToCart(Guid id, string returnController, string returnAction)
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

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
