using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookFriends.Data;
using BookFriends.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookFriends.Controllers
{
    public class CommunitiesController : Controller
    {
        private readonly BookFriendsDbContext _context;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;


        public CommunitiesController(ILogger<CommunitiesController> logger, IConfiguration configuration, BookFriendsDbContext context)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Browse");
        }

        public async Task<IActionResult> Browse()
        {
            var viewModelBuilder = new BrowseCommunitiesViewModelBuilder(_logger, _configuration, _context);
            await Task.Run(viewModelBuilder.Build);
            return View(viewModelBuilder.CommunityListings);
        }

    }
}
