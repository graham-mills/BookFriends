using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess
{
    public class BookFriendsDbInitializer
    {
        public static void Seed(BookFriendsDbContext context)
        {
            context.Database.EnsureCreated();

            // Prevent re-seeding existing data
            if (context.Users.Any())
                return;

            var dummyEntityFactory = new DummyEntityFactory();
            dummyEntityFactory.CreateEntities();

            context.Authors.AddRange(dummyEntityFactory.Authors.Values);
            context.Books.AddRange(dummyEntityFactory.Books.Values);
            context.Users.AddRange(dummyEntityFactory.Users.Values);
            context.CommunityGroups.AddRange(dummyEntityFactory.CommunityGroups.Values);

            context.SaveChanges();
        }
    }
}
