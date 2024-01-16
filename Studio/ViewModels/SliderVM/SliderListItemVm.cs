namespace Studio.ViewModels.SliderVM
{
    public class SliderListItemVm
    {
        public int Id { get; set; }
        public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Button { get; set; }
		public string ImageUrl { get; set; }
		public bool IsUpdated { get; set; } = false;
		public bool IsDeleted { get; set; } = false;
		public DateTime? CreatedAt { get; set; }
        public DateTime? UptadedAt { get; set; }
    }
}
