using System;
using Movies.Framework.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace Movies.Domain.Entities
{
    [Table("Movie")] //atributo utilizado pelo dapper
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

        [Computed] //atributo utilizado pelo dapper
        public Genre Genre { get; set; }

        [Computed] //atributo utilizado pelo dapper
        public virtual ICollection<MovieLocation> Locations { get; set; }
    }
}
