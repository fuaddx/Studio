using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Studio.Contexts;
using Studio.Models;
using Studio.ViewModels;
using Studio.ViewModels.SliderVM;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Studio.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SliderController : Controller
    {
        DataDbContext _db { get; }
        IWebHostEnvironment _environment;
        

        public SliderController(DataDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> Index()
        {
            return View(await _db.Sliders.Select(c => new SliderListItemVm
            {
                Id = c.Id,
                Description = c.Description,
                Title = c.Title,
                Button = c.Button,
                ImageUrl = c.ImageUrl,
                IsUpdated = c.IsUpdated,
                IsDeleted = c.IsDeleted,
                UptadedAt = c.UptadedAt,
                CreatedAt = c.CreatedAt,
            }).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            string filename = null;

            if (vm.MainImage != null)
            {

                filename = Guid.NewGuid() + Path.GetExtension(vm.MainImage.FileName);

                using (Stream fs = new FileStream(Path.Combine(_environment.WebRootPath, "Assets", "image", "Stories", filename), FileMode.Create))
                {
                    await vm.MainImage.CopyToAsync(fs);
                }
            }
            Slider slider = new Slider
            {
                Title = vm.Title,
                Description = vm.Description,
                Button = vm.Button,
                ImageUrl = filename,
                IsUpdated = true
            };
            _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            return View(new SliderUpdateVm
            {
                Title = data.Title,
                Description = data.Description,
                Button = data.Button,
                ImageUrl = data.ImageUrl,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, SliderUpdateVm vm)
        {
            if (id == null) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.Description = vm.Description;
            data.Button = vm.Button;
            data.Title = vm.Title;

            if (!string.IsNullOrEmpty(data.ImageUrl))
            {
                string imagePath = Path.Combine(_environment.WebRootPath, "Assets", "image", "Stories", data.ImageUrl);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            string filename = Guid.NewGuid() + Path.GetExtension(vm.MainImage.FileName);
            using (Stream fs = new FileStream(Path.Combine(_environment.WebRootPath, "Assets", "image", "Stories", filename), FileMode.Create))
            {
                await vm.MainImage.CopyToAsync(fs);
            }

            data.ImageUrl = filename;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            _db.Sliders.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
		public async Task<IActionResult> ReverseProduct(int? id)
		{
			if (id == null) return BadRequest();
			var data = await _db.Sliders.FindAsync(id);
			if (data == null) return NotFound();
			data.IsDeleted = false;
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
