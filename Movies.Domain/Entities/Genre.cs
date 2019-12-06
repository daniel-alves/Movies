using Movies.Domain.Entities;
using Movies.Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Domain
{
    public class Genre : Entity
    {
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Active { get; set; }
    }
}
