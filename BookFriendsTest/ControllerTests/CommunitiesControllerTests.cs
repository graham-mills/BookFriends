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
        private readonly Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        private readonly Mock<IConfigurationSection> mockConfigurationSection = new Mock<IConfigurationSection>();
       
        private CommunitiesController uut;
        private DummyEntityFactory entityFactory;

        public CommunitiesControllerTests()
        {
            entityFactory = new DummyEntityFactory();
            entityFactory.CreateEntities();
        }

        private void SetupMockConfiguration(int configuredPaginationAmount)
        {
            mockConfigurationSection.Setup(a => a.Value).Returns(configuredPaginationAmount.ToString());
            mockConfiguration.Setup(c => c.GetSection(It.IsAny<String>())).Returns(mockConfigurationSection.Object);
        }

        private void SetupMockCommunityGroupRepo(IEnumerable<CommunityGroup> groupsInRepo, int expectedAmountToTake)
        {
            // Return correct amount of groups if correct amount are requested
            mockCommunityGroupRepo.Setup(x => x.Get(
                It.IsAny<Expression<Func<CommunityGroup, bool>>>(),
                It.IsAny<Func<IQueryable<CommunityGroup>, IOrderedQueryable<CommunityGroup>>>(),
                It.Is<int?>(t => t == expectedAmountToTake))
            ).Returns(groupsInRepo.Take(expectedAmountToTake));
            // Returns no groups if incorrect amount are requested
            mockCommunityGroupRepo.Setup(x => x.Get(
                It.IsAny<Expression<Func<CommunityGroup, bool>>>(),
                It.IsAny<Func<IQueryable<CommunityGroup>, IOrderedQueryable<CommunityGroup>>>(),
                It.Is<int?>(t => t != expectedAmountToTake))
            ).Returns(groupsInRepo.Take(0));
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
            SetupMockConfiguration(paginationAmount);
            SetupMockCommunityGroupRepo(entityFactory.CommunityGroups.Values.Take(groupsInRepo), paginationAmount);

            // Act
            var result = uut.Browse() as ViewResult;

            // Assert
            var viewModel = result.Model as BrowseCommunitiesViewModel;
            Assert.AreEqual(expectedGroupsReturned, viewModel.CommunityGroupDtos.Count);
        }
    }
}