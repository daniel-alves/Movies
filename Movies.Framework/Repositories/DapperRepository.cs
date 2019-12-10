using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Movies.Framework.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Movies.Framework.Repositories.Dapper
{
    //implementa o repositório default desta forma é possível alternar livremente entre utilizar um repositório dapper ou um EfCore,
    //sem ser necessário alterar services e controllers
    public class DapperRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private IDbConnection _connection;

        private readonly IConfiguration _configuration;

        //recebe a mesma string de conexão utilizada pelo entity
        public DapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //abre a conexão com a base de dados 
        protected IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_configuration.GetConnectionString("MovieContext"));
            }

            return _connection;
        }

        //adiciona uma entity
        public void Add(TEntity entity)
        {
            GetConnection().Insert(entity);
        }

        //adiciona entities em massa
        public bool Add(IEnumerable<TEntity> items)
        {
            foreach(var entity in items)
                GetConnection().Insert(entity);

            return true;
        }

        //adiciona uma entity versão async
        public Task AddAsync(TEntity entity)
        {
            return GetConnection().InsertAsync(entity);
        }

        //verifica se existe uma entity com o id informado cadastrada
        public bool Exists(long id)
        {
            return Get(id) != null;
        }

        //busca uma entity pelo id
        public TEntity Get(long id)
        {
            return GetConnection().Get<TEntity>(id);
        }

        //busca uma lista paginada de entities
        public List<TEntity> GetPage(int limit, int offset)
        {
            var tableName = typeof(TEntity).Name;

            return GetConnection().Query<TEntity>($"Select * From {tableName} Order by Id Offset @offset ROWS FETCH NEXT @limit ROWS ONLY", new { offset, limit }).ToList();
        }

        //busca uma entity pelo id versão async
        public Task<TEntity> GetByIdAsync(long id)
        {
            return GetConnection().GetAsync<TEntity>(id);
        }

        //exclui a entity da base pelo seu id
        public void Remove(long id)
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;
            GetConnection().Delete(entity);
        }

        //atualiza uma entity na base de dados
        public void Update(TEntity entity)
        {
            GetConnection().Update(entity);
        }

        //atualiza entities em massa na base de dados
        public bool Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                GetConnection().Update(entity);

            return true;
        }

        //busca todas as entities por uma lista de ids
        public List<TEntity> GetAllById(long[] ids)
        {
            var tableName = typeof(TEntity).Name;

            return GetConnection().Query<TEntity>($"Select * From {tableName} WHERE Id IN @ids", new {  ids }).ToList();
        }
    }
}
