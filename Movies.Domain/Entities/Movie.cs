using System;
using Movies.Framework.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Entities
{
    public class Movie : Entity
    {
        [Required]
        public long GenreId { get; set; }

        [MinLength(5), MaxLength(150), Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool Active { get; set; }

        public Genre Genre { get; set; }

        public virtual ICollection<MovieLocation> Locations { get; set; }
    }
}
