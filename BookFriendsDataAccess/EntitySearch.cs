using BookFriendsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends
{
    /// <summary>
    /// Class provides the utility to perform a basic text search of ISearchQueryableEntity's
    /// from their corresponding repositories.
    /// </summary>
    public class EntitySearch<TEntity> where TEntity : class, ISearchQueryableEntity
    {
        private readonly IEntityRepository<TEntity> _entityRepository;

        public EntitySearch(IEntityRepository<TEntity> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        /// <summary>
        /// Attempts to composite a list of ISearchQueryableEntity objects by comparing
        /// their search queryable strings against a search query. 
        /// </summary>
        /// <returns>List of entities ordered by search query matches</returns>
        public IList<TEntity> Search(string searchQuery, int maxSearchResults)
        {
            var entityQueryResults = new Dictionary<TEntity, int>();

            string[] queryWords = searchQuery.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _entityRepository.Get().ToList().ForEach(e => entityQueryResults.Add(e, CountQueryWordHits(e, queryWords)));

            return entityQueryResults.Keys.Where(e => entityQueryResults[e] > 0).OrderBy(e => entityQueryResults[e]).Take(maxSearchResults).ToList();
        }

        /// <summary>
        /// Returns number of query words that match an entities queryable strings
        /// </summary>
        private int CountQueryWordHits(ISearchQueryableEntity entity, string[] queryWords)
        {
            int queryHits = 0;
            string[] queryableStrings = entity.GetSearchQueryableStrings();
            
            foreach(var word in queryWords)
            {
                foreach(var queryableString in queryableStrings)
                {
                    if(queryableString.ToLower().Contains(word))
                    {
                        ++queryHits;
                    }
                }
            }
            return queryHits;
        }

    }
}
