﻿using System.ComponentModel.DataAnnotations;

namespace WebDeveloperRating.Data.Models
{
    public class WebDeveloper
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
