using System;
using Movies.Framework.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Entities
{
    public class Location : Entity
    {
        [Required]
        public string Cpf { get; set; }

        [Required]
        public DateTime LocatedAt { get; set; }

        public virtual ICollection<MovieLocation> Movies { get; set; }
    }
}
