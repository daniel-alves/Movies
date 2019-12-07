using Movies.Framework.Entities;

namespace Movies.Domain.Entities
{
    public class MovieLocation : Entity
    {
        public long MovieId { get; set; }
        
        public long LocationId { get; set; }

        public Movie Movie { get; set; }

        public Location Location { get; set; }
    }
}
