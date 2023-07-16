using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using synaxnet.Data;
using synaxnet.Models;

namespace synaxnet.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext context;

        public OrderController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Order()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Order(OrderViewModel model, Guid id)
        {
            var product = await context.Products.SingleOrDefaultAsync(p => p.Id == id);
            var ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            }


            var order = new Order
                {
                    Name = model.Name,
                    Address = model.Address,
                    City = model.City,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    IpAdres = HttpContext.Connection.RemoteIpAddress.ToString(),
                    Status=OrderStatus.New

                    
                };

                context.Orders.Add(order);
                await context.SaveChangesAsync();

                TempData["success"] = "Ürün ekleme işlemi başarıyla tamamlanmıştır";
                return View();
           
        }

    }
}
