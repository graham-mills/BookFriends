using BookFriends;
using BookFriends.ApiControllers;
using BookFriends.Controllers;
using BookFriends.ViewModels;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using BookFriendsDataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace BookFriendsTest.ControllerTests
{
    public class CommunitiesControllerTests
    {
        private readonly Mock<IEntityRepository<CommunityGroup>> mockCommunityGroupRepo = new Mock<IEntityRepository<CommunityGroup>>();
        private readonly Mock<ILogger<CommunitiesController>> mockLogger = new Mock<ILogger<CommunitiesController>>();
        private readonly Mock<IBfConfiguration> mockConfiguration = new Mock<IBfConfiguration>();
       
        private CommunitiesController uut;
        private DummyEntityFactory entityFactory;

        public CommunitiesControllerTests()
        {
            entityFactory = new DummyEntityFactory();
            entityFactory.CreateEntities();
        }

        [SetUp]
        public void Setup()
        {
            uut = new CommunitiesController(mockLogger.Object, mockConfiguration.Object, mockCommunityGroupRepo.Object);
        }

        [Test]
        public void Index()
        {
            var result = uut.Index() as RedirectToActionResult;

            Assert.IsTrue(result.ActionName.Equals("Browse"));
        }

        [TestCase(0, 4, 0)]
        [TestCase(2, 0, 0)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 2)]
        [TestCase(2, 3, 2)]
        public void Browse(int listingsPerPage, int groupsInRepo, int expectedGroupsReturned)
        {
            // Arrange
            mockConfiguration.Setup(c => c.BrowseCommunitiesListingsPerPage).Returns(listingsPerPage);
            mockCommunityGroupRepo.SetupGet(entityFactory.CommunityGroups.Values.Take(groupsInRepo), listingsPerPage, expectedGroupsReturned);

            // Act
            var result = uut.Browse() as ViewResult;

            // Assert
            var viewModel = result.Model as BrowseCommunitiesViewModel;
            Assert.AreEqual(expectedGroupsReturned, viewModel.CommunityGroups.Count());
            Assert.AreEqual(listingsPerPage, viewModel.ListingsPerPage);
        }

    }
}