using BookFriends.Data.Entities;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.Data
{
    public class BookFriendsDbInitializer
    {
        public static void Seed(BookFriendsDbContext context)
        {
            context.Database.EnsureCreated();

            // Prevent re-seeding existing data
            if (context.Users.Any())
                return;

            // Add authors
            var authors = new List<Author>();
            authors.Add(new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Gilderoy",
                LastName = "Lockhart"
            });
            authors.Add(new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Libatius",
                LastName = "Borage"
            });
            authors.Add(new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Tom",
                LastName = "Riddle"
            });
            context.Authors.AddRange(authors);
            context.SaveChanges();

            // Add books
            var books = new List<Book>();
            books.Add(new Book
            {
                Id = Guid.NewGuid(),
                Name = "Who Am I?",
                Authors = new List<Author>() { authors.ElementAt(0) }
            });
            books.Add(new Book
            {
                Id = Guid.NewGuid(),
                Name = "Advanced Potion Making",
                Authors = new List<Author>() { authors.ElementAt(1) }
            });
            books.Add(new Book
            {
                Id = Guid.NewGuid(),
                Name = "My Diary",
                Authors = new List<Author>() { authors.ElementAt(2) }
            });
            context.Books.AddRange(books);
            context.SaveChanges();

            // Add users
            var users = new List<User>();
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Harry",
                EmailAddress = "h.potter@owlmail.com"
            });
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Hermione",
                EmailAddress = "h.granger@owlmail.com"
            });
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Ron",
                EmailAddress = "r.weasley@owlmail.com"
            });
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Neville",
                EmailAddress = "n.loooongbottom@owlmail.com"
            });
            context.Users.AddRange(users);
            context.SaveChanges();

            // Add communities
            var communities = new List<CommunityGroup>();
            communities.Add(new CommunityGroup
            {
                Id = Guid.NewGuid(),
                Name = "Hogwarts (Students)",
                Description = "Slytherins not allowed."
            });
            communities.Add(new CommunityGroup
            {
                Id = Guid.NewGuid(),
                Name = "Gilderoy Lockhart Appreciation Society",
                Description = "Gilderoy Lockhart, Order of Merlin, Third Class, Honorary Member of the Dark Force Defence League, and five-time winner of Witch Weekly's Most Charming Smile Award."
            });
            communities.Add(new CommunityGroup
            {
                Id = Guid.NewGuid(),
                Name = "Askaban Rehabilitation Group",
                Description = "Looking for new members with an academic interest in the dark arts."
            });
            context.CommunityGroups.AddRange(communities);
            context.SaveChanges();
        }
    }
}
