using BookFriends;
using BookFriends.ApiControllers;
using BookFriends.Controllers;
using BookFriends.ViewModels;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
        
        [TestCase(0, 1, 3, 0)]
        [TestCase(1, 1, 3, 1)]
        [TestCase(3, 1, 3, 3)]
        [TestCase(3, 1, 2, 2)]
        [TestCase(1, 3, 3, 1)]
        [TestCase(1, 1, 0, 0)]
        public void TestParameterisedGet(int itemsToGet, int pageNumber, int itemsInRepo, int expectedItemsReturned)
        {
            mockCommunityGroupRepo.SetupGet(entityFactory.CommunityGroups.Values.Take(itemsInRepo), itemsToGet);

            ActionResult<object[]> result = uut.Get(itemsToGet, pageNumber);

            Assert.AreEqual(expectedItemsReturned, result.Value.Length);
        }

        [Test]
        public void TestParameterisedGetPageZero()
        {
            int itemsInRepo = 1;
            int itemsToGet = 1;
            int pageNumber = 0;
            mockCommunityGroupRepo.SetupGet(entityFactory.CommunityGroups.Values.Take(itemsInRepo), itemsInRepo);

            ActionResult<object[]> result = uut.Get(itemsToGet, pageNumber);

            Assert.IsNull(result.Value);
        }

        [Test]
        public void TestParameterlessGet()
        {
            int itemsInRepo = 3;
            mockCommunityGroupRepo.SetupGet(entityFactory.CommunityGroups.Values.Take(itemsInRepo), itemsInRepo);

            ActionResult<object[]> result = uut.Get();

            Assert.AreEqual(result.Value.Length, 0);
        }
    }
}