using System;
using System.Collections.Generic;
using System.Linq;
using BookFriends.ApiControllers.Dtos;
using BookFriends.ViewModels;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
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
            viewModel.CommunityGroups = _communityGroupRepo.Get(take: listingsToDisplay).Select(e => new CommunityGroupDto(e));
            viewModel.TotalCommunityGroups = _communityGroupRepo.Get().Count();
            viewModel.ListingsPerPage = listingsToDisplay;
            return View(viewModel);
        }

    }
}
