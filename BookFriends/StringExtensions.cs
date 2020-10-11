using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if the string is null or length zero
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return (str == null) || (str.Length == 0);
        }
    }
}
