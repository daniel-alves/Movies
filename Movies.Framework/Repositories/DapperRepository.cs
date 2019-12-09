using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Movies.Framework.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Movies.Framework.Repositories.Dapper
{
    public class DapperRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private IDbConnection _connection;

        private readonly IConfiguration _configuration;

        public DapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_configuration.GetConnectionString("MovieContext"));
            }

            return _connection; ;
        }

        public void Add(TEntity entity)
        {
            GetConnection().Insert(entity);
        }

        public bool Add(IEnumerable<TEntity> items)
        {
            foreach(var entity in items)
                GetConnection().Insert(entity);

            return true;
        }

        public Task AddAsync(TEntity entity)
        {
            return GetConnection().InsertAsync(entity);
        }

        public bool Exists(long id)
        {
            return Get(id) != null;
        }

        public TEntity Get(long id)
        {
            return GetConnection().Get<TEntity>(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                var res = GetConnection().GetAll<TEntity>();

            } catch(Exception ex)
            {
                var a = ex.Message;
            }
            return null;
        }

        public Task<TEntity> GetByIdAsync(long id)
        {
            return GetConnection().GetAsync<TEntity>(id);
        }

        public void Remove(long id)
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;
            GetConnection().Delete(entity);
        }

        public void Update(TEntity entity)
        {
            GetConnection().Update(entity);
        }

        public bool Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                GetConnection().Update(entity);

            return true;
        }
    }
}
