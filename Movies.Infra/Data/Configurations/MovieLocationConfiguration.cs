using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infra.Data.Configurations
{
    //configuração de relacionamento da tabela MovieLocation
    public class MovieLocationConfiguration : IEntityTypeConfiguration<MovieLocation>
    {
        public void Configure(EntityTypeBuilder<MovieLocation> builder)
        {
            builder.HasIndex(t => new { t.MovieId, t.LocationId }).IsUnique();

            builder.HasOne(e => e.Movie)
                .WithMany(e => e.Locations)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
