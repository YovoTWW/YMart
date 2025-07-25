using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using YMart.Data;
using YMart.Data.Models;
using YMart.ViewModels.Order;

namespace YMart.Controllers
{
    using static YMart.Constants.DataConstants;
    public class OrderController : Controller
    {

        private readonly ApplicationDbContext dbContext;

        public OrderController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> OrderConfirmation(List<Guid> ProductIds, List<int> Quantities)
        {
            string userId = GetCurrentUserId() ?? string.Empty;

            if (ProductIds == null || Quantities == null || ProductIds.Count == 0 || Quantities.Count == 0)
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
        public async Task<IActionResult> AllOrders()
        {
            var model = await dbContext.Orders
                    .Select(o => new MiniOrderViewModel()
                    {
                        Id = o.Id,
                        TotalPrice = o.TotalPrice,
                        OrderTime = o.OrderTime.ToString(DateFormat)
                    }).AsNoTracking().ToListAsync();

            return this.View(model);
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

        private string? GetCurrentUserEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
