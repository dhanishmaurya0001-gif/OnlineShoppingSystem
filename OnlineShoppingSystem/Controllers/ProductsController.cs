using Microsoft.AspNetCore.Mvc;
using OnlineShoppingSystem.Models;

namespace OnlineShoppingSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                products =
                    products.Where(x =>
                        x.Name.Contains(search));
            }

            return View(products.ToList());
        }
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}