using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Movies.Framework.Entities;
using System.Collections.Generic;

namespace Movies.Framework.Repositories.Dapper
{
    public class DapperRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private IDbConnection _connection;

        private readonly string _connectionString;

        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
            }

            return _connection; ;
        }

        public void Add(TEntity entity)
        {
            _connection.Insert(entity);
        }

        public bool Add(IEnumerable<TEntity> items)
        {
            foreach(var entity in items)
                _connection.Insert(entity);

            return true;
        }

        public Task AddAsync(TEntity entity)
        {
            return _connection.InsertAsync(entity);
        }

        public bool Exists(long id)
        {
            return Get(id) != null;
        }

        public TEntity Get(long id)
        {
            return _connection.Get<TEntity>(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _connection.GetAll<TEntity>().AsQueryable();
        }

        public Task<TEntity> GetByIdAsync(long id)
        {
            return _connection.GetAsync<TEntity>(id);
        }

        public void Remove(long id)
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;
            _connection.Delete(entity);
        }

        public void RemoveRange(Func<TEntity, bool> where)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            _connection.Update(entity);
        }

        public bool Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                _connection.Update(entity);

            return true;
        }
    }
}
