using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookFriends.ApiControllers.Dtos;
using BookFriends.ViewModels;
using BookFriendsDataAccess.Entities;
using BookFriendsDataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookFriends.Controllers
{
    public class CommunityController : Controller
    {
        private readonly ILogger _logger;
        private readonly IBfConfiguration _configuration;
        private readonly IEntityRepository<CommunityGroup> _communityGroupRepo;
        private readonly IEntityRepository<CommunityMember> _communityMemberRepo;
        private readonly IEntityRepository<PooledBook> _pooledBookRepo;

        public CommunityController(
            ILogger<CommunityController> logger,
            IBfConfiguration configuration,
            IEntityRepository<CommunityGroup> communityGroupRepo,
            IEntityRepository<CommunityMember> communityMemberRepo,
            IEntityRepository<PooledBook> pooledBookRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _communityGroupRepo = communityGroupRepo;
            _communityMemberRepo = communityMemberRepo;
            _pooledBookRepo = pooledBookRepo;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Error", "Home");
        }

        public IActionResult View(Guid id)
        {
            var communityGroup = _communityGroupRepo.GetById(id);
            if (communityGroup == null)
                return NotFound(id);

            int membersToDisplay = _configuration.ViewCommunityMembersPerPage;
            int booksToDisplay = _configuration.ViewCommunityBooksPerPage;

            int totalPooledBooks = 0;
            foreach (var member in communityGroup.CommunityMembers)
                totalPooledBooks += member.PooledBooks.Count;

            var memberships = _communityMemberRepo.Get(filter: m => m.CommunityGroup.Id.Equals(communityGroup.Id), take: membersToDisplay).ToList();
            var pooledBooks = _pooledBookRepo.Get(filter: b => b.CommunityMember.CommunityGroup.Id.Equals(communityGroup.Id), take: booksToDisplay).ToList();

            var viewModel = new CommunityViewModel()
            {
                CommunityGroup = new CommunityGroupDto(communityGroup),
                Members = memberships.Select(e => new CommunityMemberDto(e)),
                PooledBooks = pooledBooks.Select(e => new PooledBookDto(e)),
                TotalMembers = communityGroup.CommunityMembers.Count,
                TotalPooledBooks = totalPooledBooks,
                BookListingsPerPage = booksToDisplay
            };

            return View(viewModel);
        }
    }
}
