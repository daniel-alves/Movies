using AutoMapper;
using Movies.App.Models.Genres;
using Movies.App.Models.Locations;
using Movies.App.Models.Movies;
using Movies.Domain;
using Movies.Domain.Entities;
using System.Linq;

namespace Movies.App.Mappings
{
    //auto-mapper para mapear entities para view-model e view-model para entity
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreViewModel>().ReverseMap();

            CreateMap<Movie, MovieViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Genre, opt => opt.Ignore());

            CreateMap<Location, LocationViewModel>()
                .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(e => new MovieViewModel() { Id = e.Movie.Id, Name = e.Movie.Name})))
                .ReverseMap()
                .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MoviesId.Select(movieId => new MovieLocation() { MovieId = movieId, LocationId = src.Id })));
        }
    }
}
