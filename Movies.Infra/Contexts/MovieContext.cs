using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using Movies.Domain.Entities;

namespace Movies.Infra.Contexts
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Location> Location { get; set; }
    }
}
