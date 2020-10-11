using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookFriendsDataAccess
{
    /// <summary>
    /// The purpose of this class is simply to generate dummy entity data for initial dev database seeding
    /// and unit testing.
    /// </summary>
    public class DummyEntityFactory
    {
        // Entities are stored in type maps, making it easier to setup relationships between
        // entities after constructing them, without relying on indexes.
        public enum UserType      { Harry, Hermione, Ron, Ginny, Neville, Malfoy };
        public enum CommunityType { Hogwarts, Lockhart_Society, Askaban_Rehab};
        public enum AuthorType    { Lockhart, Borage, Riddle, Bourne, Crouch};
        public enum BookType      { Who_Am_I, Potion_Making, Dear_Diary, Potente_Potions, Basic_Hexes};
        public enum MemberType    { Hogwarts_Harry, Hogwarts_Hermione, Hogwarts_Ron, Hogwarts_Ginny, 
                                    Hogwarts_Neville, Hogwarts_Malfoy, Lockhart_Society_Neville};     

        public Dictionary<AuthorType, Author> Authors { get; set; }
        public Dictionary<BookType, Book> Books { get; set; }
        public Dictionary<UserType, User> Users { get; set; }
        public Dictionary<CommunityType, CommunityGroup> CommunityGroups { get; set; }
        public Dictionary<MemberType, CommunityMember> CommunityMembers { get; set; }
        public List<OwnedBook> OwnedBooks { get; set; }
        public List<PooledBook> PooledBooks { get; set; }

        public DummyEntityFactory()
        {
            Authors = new Dictionary<AuthorType, Author>();
            Books = new Dictionary<BookType, Book>();
            Users = new Dictionary<UserType, User>();
            CommunityGroups = new Dictionary<CommunityType, CommunityGroup>();
            CommunityMembers = new Dictionary<MemberType, CommunityMember>();
            OwnedBooks = new List<OwnedBook>();
            PooledBooks = new List<PooledBook>();
        }

        public void CreateEntities()
        {
            CreateAuthors();
            CreateBooks();
            CreateUsers();
            CreateCommunityGroups();
            CreateOwnedBooks();
            CreateCommunityMembers();
        }

        private void CreateUsers()
        {
            Users.Add(UserType.Harry, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Harry",
                EmailAddress = "h.potter@owlmail.com"
            });
            Users.Add(UserType.Hermione, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Hermione",
                EmailAddress = "h.granger@owlmail.com"
            });
            Users.Add(UserType.Ron, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Ron",
                EmailAddress = "r.weasley@owlmail.com"
            });
            Users.Add(UserType.Ginny, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Ginny",
                EmailAddress = "g.weasley@owlmail.com"
            });
            Users.Add(UserType.Neville, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Neville",
                EmailAddress = "n.loooongbottom@owlmail.com"
            });
            Users.Add(UserType.Malfoy, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Malfoy",
                EmailAddress = "d.malfoy@owlmail.com"
            });
        }

        private void CreateAuthors()
        {
            Authors.Add(AuthorType.Lockhart, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Gilderoy",
                LastName = "Lockhart"
            });
            Authors.Add(AuthorType.Borage, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Libatius",
                LastName = "Borage"
            });
            Authors.Add(AuthorType.Riddle, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Tom",
                LastName = "Riddle"
            });
            Authors.Add(AuthorType.Bourne, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Phineas",
                LastName = "Bourne"
            });
            Authors.Add(AuthorType.Crouch, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Bartemius",
                LastName = "Crouch Junior"
            });
        }

        private void CreateBooks()
        {
            CreateBook(Guid.NewGuid(), BookType.Who_Am_I, "Who Am I?", AuthorType.Lockhart);
            CreateBook(Guid.NewGuid(), BookType.Potion_Making, "Advanced Potion Making", AuthorType.Borage);
            CreateBook(Guid.NewGuid(), BookType.Dear_Diary, "Dear Diary", AuthorType.Riddle);
            CreateBook(Guid.NewGuid(), BookType.Potente_Potions, "Moste Potente Potions", AuthorType.Bourne);
            CreateBook(Guid.NewGuid(), BookType.Basic_Hexes, "Basic Hexes for the Busy and Vexed", AuthorType.Crouch);
        }

        private void CreateBook(Guid id, BookType bookType, string name, AuthorType authorType)
        {
            var book = new Book() { Id = id, Name = name, Authors = new List<AuthorBook>() };
            book.Authors.Add(new AuthorBook { Author = Authors[authorType], Book = book });
            Books.Add(bookType, book);
        }

        private void CreateCommunityGroups()
        {
            CommunityGroups.Add(CommunityType.Askaban_Rehab, new CommunityGroup
            {
                Id = Guid.NewGuid(),
                Name = "Askaban Rehabilitation Group",
                Description = "Looking for new members with an academic interest in the dark arts.",
                Keywords = "dark,arts,prison"
            });
            CommunityGroups.Add(CommunityType.Lockhart_Society, new CommunityGroup
            {
                Id = Guid.NewGuid(),
                Name = "Gilderoy Lockhart Appreciation Society",
                Description = "Gilderoy Lockhart, Order of Merlin, Third Class, Honorary Member of the Dark Force Defence League, and five-time winner of Witch Weekly's Most Charming Smile Award.",
                Keywords = "celebrity,wizard,author,fans"
            });
            CommunityGroups.Add(CommunityType.Hogwarts, new CommunityGroup
            {
                Id = Guid.NewGuid(),
                Name = "Hogwarts (Students)",
                Description = "Slytherins not allowed.",
                Keywords = "magic,school,potions,divination,transfiguration,herbology"
            });
        }

        private void CreateOwnedBooks()
        {
            OwnedBook book;
            // Neville's books
            book = new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.Who_Am_I],
                Notes = "Signed special edition."
            };
            Users[UserType.Neville].OwnedBooks.Add(book);
            OwnedBooks.Add(book);
            // Harry's books
            book = new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.Potion_Making],
                Notes = "Missing pages 31-32."
            };
            Users[UserType.Harry].OwnedBooks.Add(book);
            OwnedBooks.Add(book);
            // Hermione's books
            book = new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = false,
                Book = Books[BookType.Potion_Making],
                Notes = "Unabridged version"
            };
            Users[UserType.Hermione].OwnedBooks.Add(book);
            OwnedBooks.Add(book);
            book = new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.Potente_Potions],
                Notes = "Slightly damp"
            };
            Users[UserType.Hermione].OwnedBooks.Add(book);
            OwnedBooks.Add(book);
            // Ron's books
            book = new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.Potion_Making],
                Notes = "Unread."
            };
            Users[UserType.Ron].OwnedBooks.Add(book);
            OwnedBooks.Add(book);
            // Ginny's books
            book = new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = false,
                Book = Books[BookType.Dear_Diary],
                Notes = "Only copy in the school."
            };
            Users[UserType.Ginny].OwnedBooks.Add(book);
            OwnedBooks.Add(book);
            // Malfoy's books
            book = new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.Basic_Hexes],
                Notes = ""
            };
            Users[UserType.Malfoy].OwnedBooks.Add(book);
            OwnedBooks.Add(book);
        }

        private void CreateCommunityMembers()
        {
            CreateCommunityMember(UserType.Harry   , MemberType.Hogwarts_Harry          , CommunityType.Hogwarts);
            CreateCommunityMember(UserType.Hermione, MemberType.Hogwarts_Hermione       , CommunityType.Hogwarts);
            CreateCommunityMember(UserType.Ron     , MemberType.Hogwarts_Ron            , CommunityType.Hogwarts);
            CreateCommunityMember(UserType.Ginny   , MemberType.Hogwarts_Ginny          , CommunityType.Hogwarts);
            CreateCommunityMember(UserType.Neville , MemberType.Hogwarts_Neville        , CommunityType.Hogwarts);
            CreateCommunityMember(UserType.Neville , MemberType.Lockhart_Society_Neville, CommunityType.Lockhart_Society);
            CreateCommunityMember(UserType.Malfoy  , MemberType.Hogwarts_Malfoy         , CommunityType.Hogwarts);
        }

        private void CreateCommunityMember(UserType userType, MemberType memberType, CommunityType communityType)
        {
            var member = new CommunityMember();
            member.Id = Guid.NewGuid();
            member.Role = CommunityMember.MembershipRole.Member;
            member.CommunityGroup = CommunityGroups[communityType];

            PooledBook pooledBook;
            foreach (var ownedBook in Users[userType].OwnedBooks)
            {

                pooledBook = new PooledBook
                {
                    Id = Guid.NewGuid(),
                    OwnedBook = ownedBook
                };
                member.PooledBooks.Add(pooledBook);
                PooledBooks.Add(pooledBook);
            }
            CommunityMembers.Add(memberType, member);
            Users[userType].Memberships.Add(member);
            CommunityGroups[communityType].CommunityMembers.Add(member);
        }
    }
}
