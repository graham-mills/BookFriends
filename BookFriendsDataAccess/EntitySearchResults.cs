using System;
using System.Collections.Generic;
using System.Text;

namespace BookFriendsDataAccess
{
    public class EntitySearchResults<TEntity> where TEntity : class
    {
        public IList<TEntity> MatchedEntities { get; set; }
        public int TotalMatchedEntities { get; set; }
    }
}
