using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsShoesEcommerce.Data;

namespace SportsShoesEcommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var products = await _context.Products
          .Include(p => p.Category)
          .Include(p => p.Brand)
          .Include(p => p.ProductImages)
          .Where(p => p.IsDeleted == false)
          .ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                .Include(p => p.Reviews.Where(r => r.IsApproved == true))
                .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Search(string searchText)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Where(p => p.IsDeleted == false);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                products = products.Where(p =>
                    p.Name.Contains(searchText) ||
                    p.Category.Name.Contains(searchText));
            }

            ViewBag.SearchText = searchText;

            return View("Index", await products.ToListAsync());
        }
    }
}
