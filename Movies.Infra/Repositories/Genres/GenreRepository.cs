using Dapper;
using Microsoft.Extensions.Configuration;
using Movies.Domain;
using Movies.Framework.Repositories.Dapper;
using System.Collections.Generic;
using System.Linq;

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

        public List<Genre> GetAllActiveAndContainName(string name)
        {
            var sql = "Select * from Genre Where Active = 1";

            if (!string.IsNullOrWhiteSpace(name))
                sql += " AND Name LIKE '%@name%'";

            return GetConnection().Query<Genre>(sql, new { name }).ToList();
        }
    }
}
