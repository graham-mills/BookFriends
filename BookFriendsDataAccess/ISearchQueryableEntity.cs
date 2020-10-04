using System;
using System.Collections.Generic;
using System.Text;

namespace BookFriendsDataAccess
{
    public interface ISearchQueryableEntity
    {
        /// <summary>
        /// Returns value representing the correlation between
        /// entity and query string. Lower value equalling
        /// higher correlation or likeness.
        /// </summary>
        int CalculateQueryDistance(string query);
    }
}
