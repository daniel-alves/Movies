using System;
using Movies.Framework.Entities;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Movies.Domain.Entities;

namespace Movies.Domain
{
    public class Genre : Entity
    {
        [MinLength(5), MaxLength(50), Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool Active { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
