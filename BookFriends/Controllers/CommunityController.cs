using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookFriends.ViewModels;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookFriends.Controllers
{
    public class CommunityController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IEntityRepository<CommunityGroup> _communityGroupRepo;
        private readonly IEntityRepository<CommunityMember> _communityMemberRepo;
        private readonly IEntityRepository<PooledBook> _pooledBookRepo;

        public CommunityController(
            ILogger<CommunityController> logger,
            IConfiguration configuration,
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
            if (_communityGroupRepo.GetById(id) == null)
                return NotFound(id);

            var viewModelBuilder = new CommunityViewModelBuilder(_logger, _configuration, _communityGroupRepo, _communityMemberRepo, _pooledBookRepo);
            viewModelBuilder.CommunityGroupId = id;
            viewModelBuilder.Build();

            return View(viewModelBuilder.ViewModel);
        }
    }
}
