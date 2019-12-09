using Dapper.Contrib.Extensions;
using Movies.Framework.Entities;

namespace Movies.Domain.Entities
{
    [Table("MovieLocation")]
    public class MovieLocation : Entity
    {
        public long MovieId { get; set; }
        
        public long LocationId { get; set; }

        [Computed]
        public Movie Movie { get; set; }

        [Computed]
        public Location Location { get; set; }
    }
}
