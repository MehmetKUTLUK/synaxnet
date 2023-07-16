using synaxnet.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Jpeg;
using synaxnet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Security.Claims;

namespace synaxnet.Areas.Admin.Controllers
{
    [Area("Admin")]
  
    public class ProductController : Controller
    {
        private readonly string entityName = "Masallar";
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(
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
            var query = context.Products;

            var result = new DataTableResult
            {
                data = await query
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Text,
                        p.Photo,
                        dateCreated = p.Date.ToLocalTime().ToShortDateString(),
                    }).ToListAsync(),
                draw = parameters.Draw,
                recordsFiltered = await query.CountAsync(),
                recordsTotal = await query.CountAsync()
            };

            return Json(result);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {

            if (model.PhotoFile is not null)
            {
                using var image = await Image.LoadAsync(model.PhotoFile.OpenReadStream());


                image.Mutate(p => p.Resize(new ResizeOptions
                {
                    Size = new Size(200, 200),
                    Mode = ResizeMode.Crop
                }));

                model.Photo = image.ToBase64String(JpegFormat.Instance);

            }




            context.Products.Add(model);
            context.SaveChanges();
            TempData["success"] = "Ürün ekleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = context.Products.Find(id);

            return View(model);


        }
       
        [HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {



            if (model.PhotoFile is not null)
            {
                using var image = await Image.LoadAsync(model.PhotoFile.OpenReadStream());


                image.Mutate(p => p.Resize(new ResizeOptions
                {
                    Size = new Size(250, 250),
                    Mode = ResizeMode.Crop
                }));

                model.Photo = image.ToBase64String(JpegFormat.Instance);

            }




            context.Products.Add(model);

            context.Products.Update(model);
            context.SaveChanges();
            TempData["success"] = "Ürün güncelleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));





        }

        public IActionResult Delete(Guid id)
        {
            var model = context.Products.Find(id);
            context.Products.Remove(model);
            context.SaveChanges();
            TempData["success"] = "Ürün silme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }
    }
}

