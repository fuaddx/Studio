using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studio.Contexts;
using Studio.Models;
using Studio.ViewModels.ServiceVm;

namespace Studio.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ServiceController : Controller
	{
		DataDbContext _db {  get; }

		public ServiceController(DataDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _db.Services.Select(c=> new ServiceListItemVm
			{
				Id = c.Id,
				Title = c.Title,
				Sliders = c.Sliders,
				CreatedAt = c.CreatedAt,
				UptadedAt = c.UptadedAt
			}).ToListAsync());
		}
		public IActionResult Create()
		{
			ViewBag.Slider = _db.Sliders;
			return View();
		}
		public async Task<IActionResult> Cancel()
		{
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult>Create(ServiceCreateVm vm)
		{
			
            Service service = new Service
			{
				Title = vm.Title,
				SliderId = vm.SliderId,
			};
			if (!ModelState.IsValid)
			{
				ViewBag.Slider = _db.Sliders;
				return View(vm);
			}
			_db.Services.AddAsync(service);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult>Update(int? id)
		{
            ViewBag.Slider = _db.Sliders;
            if (id == null) return BadRequest();
			var data = await _db.Services.FindAsync(id);
			if (data == null) return NotFound();
			return View(new ServiceUpdateVm
			{
				Title = data.Title,
				SliderId= data.SliderId,
			});
		}
		[HttpPost]
		public async Task<IActionResult>Update(int? id, ServiceUpdateVm vm)
		{
			if (id == null || id<0) return BadRequest();
			if (!ModelState.IsValid)
			{
				ViewBag.Slider = _db.Sliders;
				return View(vm);
			}
			var data =await _db.Services.FindAsync(id);
			if(data == null) return NotFound();
			data.Title = vm.Title;
			data.SliderId = vm.SliderId;
			data.Sliders = vm.Sliders;
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult>Delete(int? id)
		{
			if(id==null || id<0) return BadRequest();
			var data = await _db.Services.FindAsync(id);
			if (data == null) return NotFound();
			_db.Services.Remove(data);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
