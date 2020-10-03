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
    public class CommunitiesController : Controller
    {
        private readonly ILogger _logger;
        private readonly IBfConfiguration _configuration;
        private readonly IEntityRepository<CommunityGroup> _communityGroupRepo;

        public CommunitiesController(ILogger<CommunitiesController> logger, IBfConfiguration configuration, IEntityRepository<CommunityGroup> communityGroupRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _communityGroupRepo = communityGroupRepo;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Browse");
        }

        public IActionResult Browse()
        {
            int listingsToDisplay = _configuration.BrowseCommunitiesListingsPerPage;
            var viewModel = new BrowseCommunitiesViewModel();
            _communityGroupRepo.Get(take: listingsToDisplay).ToList().ForEach(cg => viewModel.CommunityGroupDtos.Add(cg.ToAnonymousDto()));
            viewModel.TotalCommunities = _communityGroupRepo.Get().Count();
            return View(viewModel);
        }

    }
}
