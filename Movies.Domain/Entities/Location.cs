using System;
using Movies.Framework.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace Movies.Domain.Entities
{
    [Table("Location")]
    public class Location : Entity
    {
        [Required]
        public string Cpf { get; set; }

        [Required]
        public DateTime LocatedAt { get; set; }

        [Computed]
        public virtual ICollection<MovieLocation> Movies { get; set; }
    }
}
