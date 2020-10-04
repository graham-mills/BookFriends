using BookFriendsDataAccess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookFriendsTest
{
    public class AlgorithmTests
    {

        [TestCase("kitten", "Saturday", 7)]
        [TestCase("kittens", "mittens", 1)]
        [TestCase("buttons", "buttons", 0)]
        [TestCase("ham sandwich", "cheese sandwich", 5)]
        [TestCase("", "", 0)]
        public void TestLevenshteinDistance(string leftString, string rightString, int expectedDistance)
        {
            int actualDistance = Algorithms.CalculateLevenshteinDistance(leftString, rightString);

            Assert.AreEqual(expectedDistance, actualDistance);
        }
    }
}
