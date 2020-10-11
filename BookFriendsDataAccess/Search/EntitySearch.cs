using BookFriendsDataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Search
{
    /// <summary>
    /// Class provides the utility to perform a basic text search of ISearchQueryableEntity's
    /// from their corresponding repositories.
    /// </summary>
    public class EntitySearch<TEntity> : IEntitySearch<TEntity> where TEntity : class, ISearchQueryableEntity
    {
        private readonly IEntityRepository<TEntity> _entityRepository;

        public EntitySearch(IEntityRepository<TEntity> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        /// <summary>
        /// Attempts to compose a list of ISearchQueryableEntity objects by comparing
        /// their search queryable strings against a search query. 
        /// </summary>
        /// <returns>List of entities ordered by search query matches</returns>
        public EntitySearchResults<TEntity> Search(
            string searchQuery,
            int resultsToTake,
            int resultsToSkip,
            Expression<Func<TEntity, bool>> entityFilter = null)
        {
            var entityQueryResults = new Dictionary<TEntity, int>();

            string[] queryWords = searchQuery.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _entityRepository.Get(filter: entityFilter).ToList().ForEach(e => entityQueryResults.Add(e, CountQueryWordHits(e, queryWords)));

            var allMatchingEntities = entityQueryResults.Keys.Where(e => entityQueryResults[e] > 0)
                                          .OrderBy(e => entityQueryResults[e])
                                          .Reverse();

            return new EntitySearchResults<TEntity>()
            {
                TotalMatchedEntities = allMatchingEntities.Count(),
                MatchedEntities = allMatchingEntities.Skip(resultsToSkip).Take(resultsToTake).ToList()
            };
        }

        /// <summary>
        /// Returns number of query words that match an entities queryable strings
        /// </summary>
        private int CountQueryWordHits(ISearchQueryableEntity entity, string[] queryWords)
        {
            int queryHits = 0;
            foreach (var word in queryWords)
            {
                foreach (var queryableString in entity.GetSearchQueryableStrings())
                {
                    if (queryableString != null && queryableString.ToLower().Contains(word))
                    {
                        ++queryHits;
                    }
                }
            }
            return queryHits;
        }

    }
}
