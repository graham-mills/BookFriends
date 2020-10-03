using BookFriends;
using BookFriends.Controllers;
using BookFriends.ViewModels;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        private void SetupMockCommunityGroupRepo(IEnumerable<CommunityGroup> itemsInRepo, int expectedItemsToTake, int expectedItemsToSkip = 0)
        {
            //// Return correct amount of groups if correct amount are requested
            //mockCommunityGroupRepo.Setup(x => x.Get(
            //    It.IsAny<Expression<Func<CommunityGroup, bool>>>(),
            //    It.IsAny<Func<IQueryable<CommunityGroup>, IOrderedQueryable<CommunityGroup>>>(),
            //    It.Is<int?>(t => t == expectedItemsToTake),
            //    It.IsAny<int?>())
            //).Returns(itemsInRepo.Take(expectedItemsToTake));
            //// Returns no groups if incorrect amount are requested
            //mockCommunityGroupRepo.Setup(x => x.Get(
            //    It.IsAny<Expression<Func<CommunityGroup, bool>>>(),
            //    It.IsAny<Func<IQueryable<CommunityGroup>, IOrderedQueryable<CommunityGroup>>>(),
            //    It.Is<int?>(t => t != expectedItemsToTake),
            //    It.IsAny<int?>())
            //).Returns(itemsInRepo.Take(0));
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
        public void Browse(int paginationAmount, int groupsInRepo, int expectedGroupsReturned)
        {
            // Arrange
            mockConfiguration.Setup(c => c.BrowseCommunitiesListingsPerPage).Returns(paginationAmount);
            mockCommunityGroupRepo.SetupGet(entityFactory.CommunityGroups.Values.Take(groupsInRepo), paginationAmount);

            // Act
            var result = uut.Browse() as ViewResult;

            // Assert
            var viewModel = result.Model as BrowseCommunitiesViewModel;
            Assert.AreEqual(expectedGroupsReturned, viewModel.CommunityGroupDtos.Count);
        }
    }
}