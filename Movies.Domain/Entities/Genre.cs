using System;
using Movies.Framework.Entities;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Movies.Domain.Entities;
using Dapper.Contrib.Extensions;

namespace Movies.Domain
{
    [Table("Genre")]
    public class Genre : Entity
    {
        [MinLength(5), MaxLength(50), Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool Active { get; set; }

        [Computed]
        public ICollection<Movie> Movies { get; set; }
    }
}
