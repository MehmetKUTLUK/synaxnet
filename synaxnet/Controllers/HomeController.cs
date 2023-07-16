using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using synaxnet.Data;
using synaxnet.Models;
using System.Diagnostics;

namespace synaxnet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewBag.MostViews = context.Products.OrderByDescending(p => p.Date).ToList();
            return View();
        }
        public async Task<IActionResult> Product(Guid id)
        {
            var product = await context.Products.SingleOrDefaultAsync(p => p.Id == id);


            return View(product);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}