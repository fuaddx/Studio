using System.ComponentModel.DataAnnotations;

namespace Studio.ViewModels.SliderVM
{
    public class SliderUpdateVm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Button { get; set; }
		public string? ImageUrl { get; set; }
		
		public IFormFile MainImage { get; set; }
		public bool IsUpdated { get; set; }
	}
}
