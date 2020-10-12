using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using BookFriendsDataAccess.Repository;
using BookFriendsDataAccess.Search;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookFriendsTest
{
    public class EntitySearchTests
    {
        private readonly Mock<IEntityRepository<CommunityGroup>> mockCommunityGroupRepo = new Mock<IEntityRepository<CommunityGroup>>();

        private EntitySearch<CommunityGroup> uut;
        private IList<CommunityGroup> repoEntities;

        public EntitySearchTests()
        {
            repoEntities = new List<CommunityGroup>()
            {
                new CommunityGroup()
                {
                    Id = Guid.NewGuid(),
                    Name = "group1",
                    Keywords = "foo"
                },
                new CommunityGroup()
                {
                    Id = Guid.NewGuid(),
                    Name = "group2",
                    Keywords = "bar"
                }
            };
        }

        [SetUp]
        public void Setup()
        {
            uut = new EntitySearch<CommunityGroup>(mockCommunityGroupRepo.Object);
        }

        // Non-matching search queries
        [TestCase("foob"    , 0, 0)]
        [TestCase(""        , 0, 0)]
        [TestCase("group3"  , 0, 0)]
        [TestCase(" "       , 0, 0)]
        // Matching search queries
        [TestCase("group1"  , 1, 1, 0)]
        [TestCase("bar"     , 1, 1, 1)]
        [TestCase("1"       , 1, 1, 0)]
        [TestCase("group"   , 2, 2, 0, 1)]
        [TestCase("foo  bar", 2, 2, 0, 1)]
        [TestCase(" 1 2 "   , 2, 2, 0, 1)]
        public void Search_QueryMatching(string searchQuery, int expectedResults, int expectedTotalResults, params int[] expectedIndexes)
        {
            int resultsToTake = repoEntities.Count;
            int resultsToSkip = 0;
            mockCommunityGroupRepo.SetupGet(repoEntities, repoEntities.Count);

            EntitySearchResults<CommunityGroup> results = uut.Search(searchQuery, resultsToTake, resultsToSkip);

            Assert.AreEqual(expectedResults, results.MatchedEntities.Count);
            Assert.AreEqual(expectedTotalResults, results.TotalMatchedEntities);
            for(int i = 0; i < expectedIndexes.Length; ++i)
            {
                Assert.IsTrue(results.MatchedEntities.Contains(repoEntities[expectedIndexes[i]]));
            }
        }

        [TestCase(1, 0, 1, 2)]
        [TestCase(1, 1, 1, 2)]
        [TestCase(1, 2, 0, 2)]
        [TestCase(3, 0, 2, 2)]
        public void Search_TakeAndSkip(int resultsToTake, int resultsToSkip, int expectedResults, int expectedTotalResults)
        {
            string searchQuery = "foo bar";
            mockCommunityGroupRepo.SetupGet(repoEntities, repoEntities.Count);

            EntitySearchResults<CommunityGroup> results = uut.Search(searchQuery, resultsToTake, resultsToSkip);

            Assert.AreEqual(expectedResults, results.MatchedEntities.Count);
            Assert.AreEqual(expectedTotalResults, results.TotalMatchedEntities);
        }

        [TestCase("foo bar bar", 1, 0)]
        [TestCase("bar foo foo", 0, 1)]
        [TestCase("group1 group2 bar", 1, 0)]
        [TestCase("group1 group2 foo", 0, 1)]
        [TestCase("1 1 2", 0, 1)]
        [TestCase("2 2 1", 1, 0)]
        public void Search_ResultOrdering(string searchQuery, params int[] expectedIndexes)
        {
            int resultsToTake = repoEntities.Count;
            int resultsToSkip = 0;
            int expectedResults = repoEntities.Count;
            int expectedTotalResults = repoEntities.Count;
            mockCommunityGroupRepo.SetupGet(repoEntities, repoEntities.Count);

            EntitySearchResults<CommunityGroup> results = uut.Search(searchQuery, resultsToTake, resultsToSkip);

            Assert.AreEqual(expectedResults, results.MatchedEntities.Count);
            Assert.AreEqual(expectedTotalResults, results.TotalMatchedEntities);
            for (int i = 0; i < expectedIndexes.Length; ++i)
            {
                if(results.MatchedEntities.Count > i)
                {
                    Assert.AreEqual(repoEntities[expectedIndexes[i]], results.MatchedEntities[i]);
                }
            }
        }
    }
}
