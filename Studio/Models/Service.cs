using System.ComponentModel.DataAnnotations.Schema;

namespace Studio.Models
{
	public class Service:BaseEntity
	{
        public string  Title  { get; set; }
        [ForeignKey("Slider")]
        public int SliderId { get; set; }
        public Slider? Sliders { get; set; }
    }
}
