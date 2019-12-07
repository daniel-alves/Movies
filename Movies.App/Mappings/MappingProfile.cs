using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.App.Models.Genres;
using Movies.App.Models.Locations;
using Movies.App.Models.Movies;
using Movies.Domain;
using Movies.Domain.Entities;

namespace Movies.App.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreViewModel>().ReverseMap();

            CreateMap<Movie, MovieViewModel>().ReverseMap();

            CreateMap<Location, LocationViewModel>().ReverseMap();
        }
    }
}
