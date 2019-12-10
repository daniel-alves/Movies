using System;
using Movies.Domain;
using System.Threading.Tasks;
using Movies.Framework.Services;
using Movies.Infra.Repositories.Genres;
using Movies.Infra.Repositories.Movies;
using System.Collections.Generic;

namespace Movies.Infra.Services.Genres
{
    //herda as funcionalidade gerais e implementa as especifidades
    public class GenreCrudService : CrudService<Genre>, IGenreCrudService
    { 
        private readonly IGenreRepository _genreRepository;

        private readonly IMovieRepository _movieRepository;

        public GenreCrudService(IGenreRepository repository, IMovieRepository movieRepository) 
            : base(repository)
        {
            _genreRepository = repository;
            _movieRepository = movieRepository;
        }

        //faz a verificação se o genêro está vinculado a um filme
        public override bool CanDelete(long id)
        {
            var movie = _movieRepository.GetByGenreId(id);
            
            return movie == null;
        }

        //insere um genêro e seta a data de criação
        public override async Task<Genre> Insert(Genre entity)
        {
            entity.CreatedAt = DateTime.Now;

            return await base.Insert(entity);
        }
        
        //atualiza os dados do genêro mantendo os dados não editáveis ou seja que não vem do formulário
        public override Genre Update(Genre entity)
        {
            var persisted = Get(entity.Id);

            persisted.Name = entity.Name;
            persisted.Active = entity.Active;

            return base.Update(persisted);
        }

        //busca todos os genêros ativos que contenham name no nome
        public List<Genre> GetAllActiveAndContainName(string name)
        {
            return _genreRepository.GetAllActiveAndContainName(name);
        }
    }
}
