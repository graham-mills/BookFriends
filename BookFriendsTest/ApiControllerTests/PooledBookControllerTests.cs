using BookFriends;
using BookFriends.ApiControllers;
using BookFriends.ApiControllers.Dtos;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using BookFriendsDataAccess.Repository;
using BookFriendsDataAccess.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookFriendsTest.ApiControllerTests
{
    public class PooledBookControllerTests
    {
        private readonly Mock<IEntityRepository<PooledBook>> mockCommunityGroupRepo = new Mock<IEntityRepository<PooledBook>>();
        private readonly Mock<ILogger<PooledBookController>> mockLogger = new Mock<ILogger<PooledBookController>>();
        private readonly Mock<IBfConfiguration> mockConfiguration = new Mock<IBfConfiguration>();
        private readonly Mock<IEntitySearch<PooledBook>> mockEntitySearch = new Mock<IEntitySearch<PooledBook>>();

        private PooledBookController uut;
        private DummyEntityFactory entityFactory;

        public PooledBookControllerTests()
        {
            entityFactory = new DummyEntityFactory();
            entityFactory.CreateEntities();
        }

        [SetUp]
        public void Setup()
        {
            uut = new PooledBookController(mockLogger.Object, mockConfiguration.Object,
                                           mockCommunityGroupRepo.Object, mockEntitySearch.Object);
        }

        [TestCase(1, 0, 1, 1)]
        [TestCase(1, 0, 0, 0)]
        [TestCase(1, 0, 2, 1)]
        [TestCase(2, 0, 1, 1)]
        [TestCase(1, 1, 2, 1)]
        [TestCase(1, 1, 1, 0)]
        public void GetByCommunityId(int limit, int offset, int itemsInRepo, int expectedItemsReturned)
        {
            mockConfiguration.Setup(m => m.BrowseCommunitiesListingsPerPage).Returns(limit);
            var dummyCommunity = entityFactory.CommunityGroups[DummyEntityFactory.CommunityType.Hogwarts];
            var dummyBook = dummyCommunity.CommunityMembers.First().PooledBooks.First();
            var repoBooks = new List<PooledBook>();
            for(int i = 0; i < itemsInRepo; ++i)
            {
                repoBooks.Add(dummyBook);
            }
            mockCommunityGroupRepo.SetupGet(repoBooks, limit, expectedItemsReturned);

            GetResult<PooledBookDto> result = uut.Get(dummyCommunity.Id, null, limit, offset);

            Assert.AreEqual(expectedItemsReturned, result.Data.Count());
            foreach (var pooledBookDto in result.Data)
            {
                Assert.AreEqual(dummyBook.Id, pooledBookDto.Id);
            }
        }
    }
}
