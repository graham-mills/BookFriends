using BookFriendsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends
{
    public class EntitySearch<TEntity> where TEntity : class, ISearchQueryableEntity
    {
        private readonly IEntityRepository<TEntity> _entityRepository;

        public EntitySearch(IEntityRepository<TEntity> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        /// <summary>
        /// Attempts to search for and collate compatible ISearchQueryableEntity objects by
        /// likeness to a search query string. 
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns>List of </returns>
        public IList<TEntity> Search(string searchQuery, int maximumSearchQueryDistance)
        {
            var matchedResults = new Dictionary<TEntity, int>();

            _entityRepository.Get().ToList().ForEach(e => matchedResults.Add(e, e.CalculateQueryDistance(searchQuery)));

            return matchedResults.Keys.Where(e => matchedResults[e] <= maximumSearchQueryDistance).OrderBy(e => matchedResults[e]).ToList();
        }

    }
}
