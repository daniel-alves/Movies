using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using Movies.Domain.Entities;

namespace Movies.Infra.Contexts
{
    public class MovieContext : DbContext
    {
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Location> Location { get; set; }

        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieLocation>().HasKey(t => new { t.MovieId, t.LocationId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
