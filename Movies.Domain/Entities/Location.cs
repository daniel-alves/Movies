using Movies.Framework.Entities;
using System;
using System.Collections.Generic;

namespace Movies.Domain.Entities
{
    public class Location : Entity
    {
        public string Cpf { get; set; }

        public DateTime LocatedAt { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
