using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studio.Contexts;
using Studio.ViewModels.SliderVM;
namespace Studio.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        DataDbContext _db { get; }

        public SliderViewComponent(DataDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _db.Sliders.Select(c => new SliderListItemVm
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Button = c.Button,
                ImageUrl = c.ImageUrl,
                IsUpdated = c.IsUpdated,
            }).ToListAsync());
        }
    }
}
