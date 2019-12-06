using Movies.Framework.Entities;
using System;

namespace Movies.Domain.Entities
{
    public class Movie : Entity
    {
        public long GenreId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Active { get; set; }

        public Genre Genre { get; set; }
    }
}
