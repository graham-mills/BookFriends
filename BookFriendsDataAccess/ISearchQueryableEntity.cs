using System;
using System.Collections.Generic;
using System.Text;

namespace BookFriendsDataAccess
{
    public interface ISearchQueryableEntity
    {
        /// <summary>
        /// Returns selected string data that we want the
        /// entity to be discovered by when the user enters
        /// a search query. 
        /// </summary>
        IEnumerable<string> GetSearchQueryableStrings();
    }
}
