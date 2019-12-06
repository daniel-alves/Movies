using Movies.Domain.Entities;
using Movies.Framework.Entities;
using System;

namespace Movies.Domain
{
    public class Genre : Entity
    {
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Active { get; set; }
    }
}
