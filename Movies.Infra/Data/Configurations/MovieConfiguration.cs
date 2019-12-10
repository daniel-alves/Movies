using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infra.Data.Configurations
{
    //configurações de relacionamento da entity Movie
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasOne(e => e.Genre)
                .WithMany(e => e.Movies)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
