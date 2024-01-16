using Studio.Models;

namespace Studio.ViewModels.ServiceVm
{
	public class ServiceListItemVm
	{
		public int Id { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UptadedAt { get; set; }
		public string Title { get; set; }
		public bool IsInSlider { get; set; }
		public int SliderId { get; set; }
		public Slider? Sliders { get; set; }
	}
}
