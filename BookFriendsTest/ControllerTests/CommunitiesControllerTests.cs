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

        [Test]
        public void Browse_ZeroCommunityRecords()
        {
            // Arrange
            var communityGroups = new List<CommunityGroup>();
            const int paginationAmount = 2;
            mockConfigurationSection.Setup(a => a.Value).Returns(paginationAmount.ToString());
            mockConfiguration.Setup(c => c.GetSection(It.IsAny<String>())).Returns(mockConfigurationSection.Object);

            mockCommunityGroupRepo.Setup(x => x.Get(It.IsAny<Expression<Func<CommunityGroup, bool>>>(),
                                                    It.IsAny<Func<IQueryable<CommunityGroup>, IOrderedQueryable<CommunityGroup>>>(),
                                                    It.IsAny<int?>()
                                                    )).Returns(communityGroups);

            // Act
            var result = uut.Browse() as ViewResult;

            // Assert
            var viewModel = result.Model as BrowseCommunitiesViewModel;
            Assert.AreEqual(0, viewModel.Communities.Count);
        }
    }
}