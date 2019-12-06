using Movies.Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Entities
{
    public class Movie : Entity
    {
        public long GenreId { get; set; }

        [MinLength(5), MaxLength(150)]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Active { get; set; }

        public Genre Genre { get; set; }
    }
}
