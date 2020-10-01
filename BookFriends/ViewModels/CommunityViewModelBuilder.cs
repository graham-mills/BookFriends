using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ViewModels
{
    public class CommunityViewModelBuilder
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IEntityRepository<CommunityGroup> _communityGroupRepo;
        private readonly IEntityRepository<CommunityMember> _communityMemberRepo;
        private readonly IEntityRepository<PooledBook> _pooledBookRepo;

        public Guid CommunityGroupId { get; set; }
        public CommunityViewModel ViewModel { get; private set; }

        public CommunityViewModelBuilder(
            ILogger logger,
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

        public void Build()
        {
            if (CommunityGroupId == Guid.Empty)
                throw new ArgumentException("CommunityGroupId");

            ViewModel = new CommunityViewModel();
            ViewModel.CommunityGroup = _communityGroupRepo.GetById(CommunityGroupId);

            int membersToDisplay = _configuration.GetValue<int>(ConfigurationKeys.ViewCommunityMembersPaginationSize);
            ViewModel.Memberships = _communityMemberRepo.Get(filter: m => m.CommunityGroup.Id.Equals(CommunityGroupId), take: membersToDisplay).ToList();

            int booksToDisplay = _configuration.GetValue<int>(ConfigurationKeys.ViewCommunityBooksPaginationSize);
            ViewModel.PooledBooks = _pooledBookRepo.Get(filter: b => b.CommunityMember.CommunityGroup.Id.Equals(CommunityGroupId), take: booksToDisplay).ToList();
        }

    }
}
