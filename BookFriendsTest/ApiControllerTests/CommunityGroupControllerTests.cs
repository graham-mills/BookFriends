using BookFriends;
using BookFriends.ApiControllers;
using BookFriends.ApiControllers.Dtos;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
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
    public class CommunityGroupControllerTests
    {
        private readonly Mock<IEntityRepository<CommunityGroup>> mockCommunityGroupRepo = new Mock<IEntityRepository<CommunityGroup>>();
        private readonly Mock<ILogger<CommunityGroupController>> mockLogger = new Mock<ILogger<CommunityGroupController>>();
        private readonly Mock<IBfConfiguration> mockConfiguration = new Mock<IBfConfiguration>();

        private CommunityGroupController uut;
        private DummyEntityFactory entityFactory;

        public CommunityGroupControllerTests()
        {
            entityFactory = new DummyEntityFactory();
            entityFactory.CreateEntities();
        }

        [SetUp]
        public void Setup()
        {
            uut = new CommunityGroupController(mockLogger.Object, mockConfiguration.Object, mockCommunityGroupRepo.Object);
        }

        [TestCase(1, 0, 1, 1)]
        [TestCase(1, 0, 0, 0)]
        [TestCase(1, 0, 2, 1)]
        [TestCase(2, 0, 1, 1)]
        [TestCase(1, 1, 2, 1)]
        [TestCase(1, 1, 1, 0)]
        public void GetWithoutQuery(int limit, int offset, int itemsInRepo, int expectedItemsReturned)
        {
            mockConfiguration.Setup(m => m.BrowseCommunitiesListingsPerPage).Returns(limit);
            mockCommunityGroupRepo.SetupGet(entityFactory.CommunityGroups.Values.Take(itemsInRepo), limit, expectedItemsReturned);

            GetResult<CommunityGroupDto> result = uut.Get(null, limit, offset);

            Assert.AreEqual(expectedItemsReturned, result.Data.Count());
        }
    }
}
