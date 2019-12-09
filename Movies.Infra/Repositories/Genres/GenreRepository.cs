using Dapper;
using Microsoft.Extensions.Configuration;
using Movies.Domain;
using Movies.Framework.Repositories.Dapper;

namespace Movies.Infra.Repositories.Genres
{
    public class GenreRepository : DapperRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public Genre GetByName(string genreName)
        {
            return GetConnection().QueryFirstOrDefault<Genre>("Select * from Genre Where Name = @genreName", new { genreName });
        }
    }
}
