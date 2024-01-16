using System.ComponentModel.DataAnnotations;

namespace Studio.ViewModels.SliderVM
{
    public class SliderCreateVm
    {
		public IFormFile? MainImage { get; set; }
		public bool IsUpdated { get; set; } = false;
		public string Title { get; set; }
        public string Description { get; set; }
        public string Button { get; set; }
    }
}
