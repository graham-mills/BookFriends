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
    public class CommunityControllerTests
    {
        private readonly Mock<IEntityRepository<CommunityGroup>> mockCommunityGroupRepo = new Mock<IEntityRepository<CommunityGroup>>();
        private readonly Mock<IEntityRepository<PooledBook>> mockPooledBookRepo = new Mock<IEntityRepository<PooledBook>>();
        private readonly Mock<IEntityRepository<CommunityMember>> mockCommunityMemberRepo = new Mock<IEntityRepository<CommunityMember>>();
        private readonly Mock<ILogger<CommunityController>> mockLogger = new Mock<ILogger<CommunityController>>();
        private readonly Mock<IBfConfiguration> mockConfiguration = new Mock<IBfConfiguration>();
       
        private CommunityController uut;
        private DummyEntityFactory entityFactory;

        public CommunityControllerTests()
        {
            entityFactory = new DummyEntityFactory();
            entityFactory.CreateEntities();
        }

        [SetUp]
        public void Setup()
        {
            uut = new CommunityController(mockLogger.Object,
                                          mockConfiguration.Object,
                                          mockCommunityGroupRepo.Object,
                                          mockCommunityMemberRepo.Object,
                                          mockPooledBookRepo.Object);
        }

        [Test]
        public void Index()
        {
            var result = uut.Index() as RedirectToActionResult;

            Assert.IsTrue(result.ActionName.Equals("Error"));
        }

        [TestCase(4, 2)]
        [TestCase(2, 1)]
        public void View(int bookListingsPerPage, int memberListingsPerPage)
        {
            var communityGroup = entityFactory.CommunityGroups[DummyEntityFactory.CommunityType.Hogwarts];
            var communityBooks = entityFactory.PooledBooks.Where(e => e.CommunityMember.CommunityGroup == communityGroup);
            mockConfiguration.Setup(c => c.ViewCommunityBooksPerPage).Returns(bookListingsPerPage);
            mockConfiguration.Setup(c => c.ViewCommunityMembersPerPage).Returns(memberListingsPerPage);
            mockCommunityGroupRepo.Setup(m => m.GetById(It.Is<Guid>(g => g.Equals(communityGroup.Id)))).Returns(communityGroup);
            mockPooledBookRepo.SetupGet(communityBooks, bookListingsPerPage);
            mockCommunityMemberRepo.SetupGet(communityGroup.CommunityMembers, memberListingsPerPage, memberListingsPerPage);

            // Act
            var result = uut.View(communityGroup.Id) as ViewResult;

            // Assert
            var viewModel = result.Model as CommunityViewModel;
            Assert.AreEqual(bookListingsPerPage, viewModel.PooledBooks.Count());
            Assert.AreEqual(memberListingsPerPage, viewModel.Members.Count());
            Assert.AreEqual(bookListingsPerPage, viewModel.BookListingsPerPage);
            Assert.AreEqual(memberListingsPerPage, viewModel.MemberListingsPerPage);
            Assert.AreEqual(communityGroup.Id, viewModel.CommunityGroup.Id);
        }

    }
}