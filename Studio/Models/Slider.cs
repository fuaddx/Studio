using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
    public class Slider:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Button { get; set; }
        public string? ImageUrl { get; set; }
		public bool IsUpdated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
		public IEnumerable<Service> Services { get; set; }
    }
}
