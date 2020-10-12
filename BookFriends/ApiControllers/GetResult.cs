using BookFriendsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ApiControllers
{
    public class GetResult<TDomainTransferObject>
    {
        public IEnumerable<TDomainTransferObject> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
