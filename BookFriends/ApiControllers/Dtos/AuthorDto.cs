using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ApiControllers.Dtos
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }

        public AuthorDto(Author entity)
        {
            Id = entity.Id;
            FirstNames = entity.FirstNames;
            LastName = entity.LastName;
        }
    }
}
