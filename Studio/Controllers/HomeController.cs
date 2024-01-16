using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studio.Contexts;
using Studio.Models;
using Studio.ViewModels.HomeVM;
using Studio.ViewModels.SliderVM;
using System.Diagnostics;

namespace Studio.Controllers
{
    public class HomeController : Controller
    {
        DataDbContext _db { get; }

        public HomeController(DataDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Sliders = await _db.Sliders.Select(c => new SliderListItemVm
                {
                    Id = c.Id,
                    Description = c.Description,
                    Title = c.Title,
                    Button = c.Button,
                    ImageUrl = c.ImageUrl,
                    IsUpdated = c.IsUpdated,
                }).ToListAsync()
            };
            return View(homeVm);
            
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
