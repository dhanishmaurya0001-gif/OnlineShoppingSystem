using Microsoft.AspNetCore.Mvc;
using OnlineShoppingSystem.Helpers;
using OnlineShoppingSystem.Models;

namespace OnlineShoppingSystem.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AddToCart(int id)
        {
            var product =
                _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var cart =
                HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart")
                ?? new List<CartItem>();

            var item =
                cart.FirstOrDefault(x => x.ProductId == id);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }

            HttpContext.Session
                .SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart =
                HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart")
                ?? new List<CartItem>();

            return View(cart);
        }
        public IActionResult Remove(int id)
        {
            var cart =
                HttpContext.Session
                .GetObjectFromJson<List<CartItem>>("Cart");

            var item =
                cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                cart.Remove(item);
            }

            HttpContext.Session
                .SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            return View();
        }
    }
}