using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BookFriendsDataAccess.Repository
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? take = null,
            int? skip = null);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
        void Delete(TEntity entity);
        int Count();
    }
}
