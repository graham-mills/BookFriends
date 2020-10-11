using System;
using System.Linq.Expressions;

namespace BookFriendsDataAccess.Search
{
    public interface IEntitySearch<TEntity> where TEntity : class, ISearchQueryableEntity
    {
        EntitySearchResults<TEntity> Search(string searchQuery, int resultsToTake, int resultsToSkip, Expression<Func<TEntity, bool>> entityFilter = null);
    }
}