using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.App.Models.Movies
{
    public class MovieViewModel : ViewModel
    {
        [Display(Name = "Genêro")]
        public long GenreId { get; set; }

        [Display(Name = "Genêro")]
        public string GenreName { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }
        
        [Display(Name = "Data de criação")]
        public DateTime CreatedAt { get; set; }
    }
}
