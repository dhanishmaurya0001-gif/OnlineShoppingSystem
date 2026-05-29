using Microsoft.AspNetCore.Mvc;
using OnlineShoppingSystem.Models;

namespace OnlineShoppingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Dashboard()
        {
            ViewBag.ProductCount =
                _context.Products.Count();

            return View();
        }
    }
}