using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Repositories.Interfaces
{
    public interface IRepository<TEntity, in TPrimaryKey> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        TEntity GetById(TPrimaryKey id);

        Task<List<TEntity>> GetAllWhere(Expression<Func<TEntity, bool>> predicate);

        void Insert(TEntity entity);

        void BatchInsert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TPrimaryKey id);
    }
}
