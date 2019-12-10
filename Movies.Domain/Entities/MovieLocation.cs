using Dapper.Contrib.Extensions;
using Movies.Framework.Entities;

namespace Movies.Domain.Entities
{
    [Table("MovieLocation")] //atributo utilizado pelo dapper
    public class MovieLocation : Entity
    {
        public long MovieId { get; set; }
        
        public long LocationId { get; set; }

        [Computed] //atributo utilizado pelo dapper
        public Movie Movie { get; set; }

        [Computed] //atributo utilizado pelo dapper
        public Location Location { get; set; }
    }
}
