using Movies.App.Models.Movies;
using Movies.App.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.App.Models.Locations
{
    public class LocationViewModel : ViewModel
    {
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Display(Name = "Filmes")]
        public long[] MoviesId { get; set; }

        [Display(Name = "Data da locação")]
        public DateTime LocatedAt { get; set; }

        public IEnumerable<MovieViewModel> Movies { get; set; }
    }
}
