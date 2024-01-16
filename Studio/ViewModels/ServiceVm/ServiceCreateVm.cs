using Studio.Models;

namespace Studio.ViewModels.ServiceVm
{
	public class ServiceCreateVm
	{
		public string Title { get; set; }
		public int SliderId { get; set; }
		public Slider? Sliders { get; set; }

	}
}
