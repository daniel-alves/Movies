using System.Linq;
using System.Threading.Tasks;
using Movies.Framework.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Movies.Framework.Repositories;

namespace Movies.Infra.Repositories
{
    //recebe o contexto a ser utilizado pensando na modularização e na funcionalidade dos bounded contexts para melhorar a performance
    //desta forma é possível manter as entities separadas por contexto e utilizar a mesma API de repositórios
    public class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {
        private readonly TContext _context;

        protected readonly DbSet<TEntity> DbSet;

        //comita os dados do contexto para a base
        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        //comita os dados do contexto para a base versão async
        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public EfCoreRepository(TContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        //insere uma entity na base de dados
        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
            SaveChanges();
        }

        //insere uma entity na base de dados versão async
        public async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj);
            await SaveChangesAsync();
        }

        //busca uma entity pelo id na base de dados versão async
        public virtual Task<TEntity> GetByIdAsync(long id)
            => DbSet.FindAsync(id);

        //busca uma entity pelo id na base de dados
        public virtual TEntity Get(long id)
            => DbSet.Find(id);

        //busca uma lista paginada de entities na base de dados
        public virtual List<TEntity> GetPage(int limit, int offset)
        {
            return DbSet.Take(limit).Skip(offset).ToList();
        }

        //atualiza uma entity na base de dados
        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
            SaveChanges();
        }

        //verifica se a entity esta cadastrada pelo seu id
        public bool Exists(long id)
        {
            if (id == 0) return false;

            return DbSet.Any(e => e.Id == id);
        }

        //exclui uma entity pelo seu id
        public void Remove(long id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
            {
                DbSet.Remove(entity);
                SaveChanges();
            }
        }

        //insere uma lista de entities na base de dados
        public bool Add(IEnumerable<TEntity> items)
        {
            var list = items.ToList();
            foreach (var item in list)
            {
                DbSet.Add(item);
            }
            SaveChanges();
            return true;
        }

        //atualiza uma lista de entities na base de dados
        public bool Update(IEnumerable<TEntity> entities)
        {
            var list = entities.ToList();

            foreach (var item in list)
            {
                DbSet.Update(item);
            }
            _context.SaveChanges();
            return true;
        }

        //busca uma lista de entities na base de dados através de seu id
        public List<TEntity> GetAllById(long[] ids)
        {
            return DbSet.Where(e => ids.Contains(e.Id)).ToList();
        }
    }
}
