using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using Movies.Domain.Entities;
using Movies.Infra.Data.Configurations;

namespace Movies.Infra.Data.Contexts
{
    //contexto na nossa necessidade mais contextos podem ser inseridos separando as entities de uma maneira mais inteligente
    public class MovieContext : IdentityDbContext
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
            modelBuilder.ApplyConfiguration(new MovieConfiguration());

            modelBuilder.ApplyConfiguration(new MovieLocationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
