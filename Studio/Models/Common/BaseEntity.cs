﻿namespace Studio.Models.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UptadedAt { get; set; }
    }
}
