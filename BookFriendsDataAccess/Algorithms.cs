using System;
using System.Collections.Generic;
using System.Text;

namespace BookFriendsDataAccess
{
    public static class Algorithms
    {
        /// <summary>
        /// Implementation of Wagner-Fischer algorithm to compute Levenstein distance between
        /// two strings. Algorithm implemented using pseudo-code from
        /// https://en.wikipedia.org/wiki/Wagner%E2%80%93Fischer_algorithm
        /// </summary>
        /// <returns>Edit distance</returns>
        public static int CalculateLevenshteinDistance(string s, string t)
        {
            int m = s.Length;
            int n = t.Length;

            var d = new int[m + 1, n + 1];

            for(int i = 1; i <= m; ++i)
            {
                d[i, 0] = i;
            }

            for(int j = 1; j <= n; ++j)
            {
                d[0, j] = j;
            }

            for (int j = 1; j <= n; ++j)
            {
                for(int i = 1; i <= m; ++i)
                {
                    d[i, j] = Minimum((d[i - 1, j] + 1), // deletion
                                      (d[i, j - 1] + 1), // insertion
                                      ((d[i - 1, j - 1]) + (s[i - 1] == t[j - 1] ? 0 : 1))); // substitution
                }
            }

            return d[m, n];
        }

        private static int Minimum(int i1, int i2, int i3)
        {
            return Math.Min(i1, Math.Min(i2, i3));
        }
    }

}
