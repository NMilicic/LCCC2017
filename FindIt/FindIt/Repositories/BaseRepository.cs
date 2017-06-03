using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;

namespace FindIt.Repositories
{
    public class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class
    {

        private readonly GuessDBEntities _db = new GuessDBEntities();

        public void Delete(TPrimaryKey id)
        {
            _db.Set<TEntity>().Remove(GetById(id));
            _db.SaveChanges();
        }

        public Task<List<TEntity>> GetAll()
        {
            return _db.Set<TEntity>().ToListAsync();
        }

        public Task<List<TEntity>> GetAllWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public TEntity GetById(TPrimaryKey id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
            _db.SaveChanges();
        }

        public void BatchInsert(IEnumerable<TEntity> entities)
        {
            _db.Set<TEntity>().AddRange(entities);
            _db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}