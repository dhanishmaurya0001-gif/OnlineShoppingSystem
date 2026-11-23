using Microsoft.AspNetCore.Mvc;
using OnlineShoppingSystem.Helpers;
using OnlineShoppingSystem.Models;

namespace OnlineShoppingSystem.Controllers;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    public CartController(ApplicationDbContext context) { _context = context; }
    private List<CartItem> ReadCart() => HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new();
    private void SaveCart(List<CartItem> cart) => HttpContext.Session.SetObjectAsJson("Cart", cart);
    public IActionResult Index() => View(ReadCart());
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult AddToCart(int id) {
        var product = _context.Products.Find(id);
        if (product is null) return NotFound();
        var cart = ReadCart();
        var item = cart.FirstOrDefault(x => x.ProductId == id);
        if (item is null) cart.Add(new CartItem { ProductId=product.Id, ProductName=product.Name, Price=product.Price, Quantity=1 });
        else item.Quantity = Math.Min(item.Quantity + 1, 99);
        SaveCart(cart); return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult UpdateQuantity(int id, int quantity) {
        if (quantity < 1 || quantity > 99) return BadRequest("Quantity must be between 1 and 99.");
        var cart = ReadCart(); var item = cart.FirstOrDefault(x => x.ProductId == id);
        if (item is null) return NotFound();
        item.Quantity = quantity; SaveCart(cart); return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Remove(int id) {
        var cart = ReadCart(); cart.RemoveAll(x => x.ProductId == id);
        SaveCart(cart); return RedirectToAction(nameof(Index));
    }
    public IActionResult Checkout() => View(ReadCart());
}
