using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.App.Models.Genres
{
    public class GenreViewModel : ViewModel
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }
        
        [Display(Name = "Data de criação")]
        public DateTime CreatedAt { get; set; }
    }
}
