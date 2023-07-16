using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using synaxnet.Areas.Admin.Models;
using synaxnet.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace synaxnet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly string entityName = "Masallar";
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public OrderController(
            AppDbContext context,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment
        )
        {
            this.context = context;
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TableData(Guid? id, DataTableParameters parameters)
        {
            var query = context.Orders;

            var result = new DataTableResult
            {
                data = await query
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .Select(p => new
                    {
                        p.Id,
                        p.Address,
                        p.Name,
                        p.City,
                        p.Email,
                        Status = p.Status.ToString(), // Status değerini string olarak dönüştür
                       
                        ipadres=p.IpAdres,
                        p.PhoneNumber,
                        dateCreated = p.Date.ToLocalTime().ToShortDateString(),
                    }).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = await query.CountAsync(),
                recordsTotal = await query.CountAsync()
            };

            return Json(result);
        }

        [HttpPost]
        public IActionResult UpdateStatus(Guid[] orderIds, string status)
        {
            if (orderIds == null || orderIds.Length == 0 || string.IsNullOrEmpty(status))
            {
                return BadRequest();
            }

            var orders = context.Orders.Where(o => orderIds.Contains(o.Id)).ToList();

            foreach (var order in orders)
            {
                if (Enum.TryParse(status, out OrderStatus orderStatus))
                {
                    order.Status = orderStatus;
                }
            }

            context.SaveChanges();

            return Ok();
        }
    }
}
