using System;
using Movies.Framework.Entities;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Movies.Domain.Entities;
using Dapper.Contrib.Extensions;

namespace Movies.Domain
{
    [Table("Genre")] //utilizado pelo dapper
    public class Genre : Entity
    {
        [MinLength(5), MaxLength(50), Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool Active { get; set; }

        [Computed] //atributo utilizado pelo dapper, indica para que ele não tente inserir esta informação no banco
        public ICollection<Movie> Movies { get; set; }
    }
}
