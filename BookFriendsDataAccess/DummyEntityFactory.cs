using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookFriendsDataAccess
{
    /// <summary>
    /// This classes' purpose is simply to generate dummy entity data for initial dev database seeding
    /// and unit testing.
    /// </summary>
    public class DummyEntityFactory
    {
        // Entities are stored in type maps, making it easier to setup relationships between
        // entities after constructing them, without relying on indexes.
        public enum UserType      { HARRY, HERMIONE, RON, GINNY, NEVILLE, MALFOY };
        public enum CommunityType { HOGWARTS, LOCKHART_SOCIETY, ASKABAN_REHAB};
        public enum AuthorType    { LOCKHART, BORAGE, RIDDLE, BOURNE, CROUCH};
        public enum BookType      { WHO_AM_I, POTION_MAKING, DEAR_DIARY, POTENTE_POTIONS, BASIC_HEXES};
        public enum MemberType    { HOGWARTS__HARRY, HOGWARTS__HERMIONE, HOGWARTS__RON, HOGWARTS__GINNY, 
                                    HOGWARTS__NEVILLE, HOGWARTS__MALFOY, LOCKHART_SOCIETY__NEVILLE};     

        public Dictionary<AuthorType, Author> Authors { get; set; }
        public Dictionary<BookType, Book> Books { get; set; }
        public Dictionary<UserType, User> Users { get; set; }
        public Dictionary<CommunityType, CommunityGroup> CommunityGroups { get; set; }
        public Dictionary<MemberType, CommunityMember> CommunityMembers { get; set; }

        public DummyEntityFactory()
        {
            Authors = new Dictionary<AuthorType, Author>();
            Books = new Dictionary<BookType, Book>();
            Users = new Dictionary<UserType, User>();
            CommunityGroups = new Dictionary<CommunityType, CommunityGroup>();
            CommunityMembers = new Dictionary<MemberType, CommunityMember>();
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
            Users.Add(UserType.HARRY, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Harry",
                EmailAddress = "h.potter@owlmail.com"
            });
            Users.Add(UserType.HERMIONE, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Hermione",
                EmailAddress = "h.granger@owlmail.com"
            });
            Users.Add(UserType.RON, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Ron",
                EmailAddress = "r.weasley@owlmail.com"
            });
            Users.Add(UserType.GINNY, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Ginny",
                EmailAddress = "g.weasley@owlmail.com"
            });
            Users.Add(UserType.NEVILLE, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Neville",
                EmailAddress = "n.loooongbottom@owlmail.com"
            });
            Users.Add(UserType.MALFOY, new User
            {
                Id = Guid.NewGuid(),
                DisplayName = "Malfoy",
                EmailAddress = "d.malfoy@owlmail.com"
            });
        }

        private void CreateAuthors()
        {
            Authors.Add(AuthorType.LOCKHART, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Gilderoy",
                LastName = "Lockhart"
            });
            Authors.Add(AuthorType.BORAGE, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Libatius",
                LastName = "Borage"
            });
            Authors.Add(AuthorType.RIDDLE, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Tom",
                LastName = "Riddle"
            });
            Authors.Add(AuthorType.BOURNE, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Phineas",
                LastName = "Bourne"
            });
            Authors.Add(AuthorType.CROUCH, new Author
            {
                Id = Guid.NewGuid(),
                FirstNames = "Bartemius",
                LastName = "Crouch Junior"
            });
        }

        private void CreateBooks()
        {
            Books.Add(BookType.WHO_AM_I, new Book
            {
                Id = Guid.NewGuid(),
                Name = "Who Am I?",
                Authors = new List<Author>() { Authors[AuthorType.LOCKHART] }
            });
            Books.Add(BookType.POTION_MAKING, new Book
            {
                Id = Guid.NewGuid(),
                Name = "Advanced Potion Making",
                Authors = new List<Author>() { Authors[AuthorType.BORAGE] }
            });
            Books.Add(BookType.DEAR_DIARY, new Book
            {
                Id = Guid.NewGuid(),
                Name = "Dear Diary",
                Authors = new List<Author>() { Authors[AuthorType.RIDDLE] }
            });
            Books.Add(BookType.POTENTE_POTIONS, new Book
            {
                Id = Guid.NewGuid(),
                Name = "Moste Potente Potions",
                Authors = new List<Author>() { Authors[AuthorType.BOURNE] }
            });
            Books.Add(BookType.BASIC_HEXES, new Book
            {
                Id = Guid.NewGuid(),
                Name = "Basic Hexes for the Busy and Vexed",
                Authors = new List<Author>() { Authors[AuthorType.CROUCH] }
            });
        }

        private void CreateCommunityGroups()
        {
            CommunityGroups.Add(CommunityType.HOGWARTS, new CommunityGroup
            {
                Id = Guid.NewGuid(),
                Name = "Hogwarts (Students)",
                Description = "Slytherins not allowed."
            });
            CommunityGroups.Add(CommunityType.LOCKHART_SOCIETY, new CommunityGroup
            {
                Id = Guid.NewGuid(),
                Name = "Gilderoy Lockhart Appreciation Society",
                Description = "Gilderoy Lockhart, Order of Merlin, Third Class, Honorary Member of the Dark Force Defence League, and five-time winner of Witch Weekly's Most Charming Smile Award."
            });
            CommunityGroups.Add(CommunityType.ASKABAN_REHAB, new CommunityGroup
            {
                Id = Guid.NewGuid(),
                Name = "Askaban Rehabilitation Group",
                Description = "Looking for new members with an academic interest in the dark arts."
            });
        }

        private void CreateOwnedBooks()
        {
            // Neville's books
            Users[UserType.NEVILLE].OwnedBooks.Add(new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.WHO_AM_I],
                Notes = "Signed special edition."
            });
            // Harry's books
            Users[UserType.HARRY].OwnedBooks.Add(new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.POTION_MAKING],
                Notes = "Missing pages 31-32."
            });
            // Hermione's books
            Users[UserType.HERMIONE].OwnedBooks.Add(new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = false,
                Book = Books[BookType.POTION_MAKING],
                Notes = "Unabridged version"
            });
            Users[UserType.HERMIONE].OwnedBooks.Add(new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.POTENTE_POTIONS],
                Notes = "Slightly damp"
            });
            // Ron's books
            Users[UserType.RON].OwnedBooks.Add(new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.POTION_MAKING],
                Notes = "Unread."
            });
            // Ginny's books
            Users[UserType.GINNY].OwnedBooks.Add(new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = false,
                Book = Books[BookType.DEAR_DIARY],
                Notes = "Only copy in the school."
            });
            // Malfoy's books
            Users[UserType.MALFOY].OwnedBooks.Add(new OwnedBook()
            {
                Id = Guid.NewGuid(),
                Available = true,
                Book = Books[BookType.BASIC_HEXES],
                Notes = ""
            });
        }

        private void CreateCommunityMembers()
        {
            CreateCommunityMember(UserType.HARRY   , MemberType.HOGWARTS__HARRY          , CommunityType.HOGWARTS);
            CreateCommunityMember(UserType.HERMIONE, MemberType.HOGWARTS__HERMIONE       , CommunityType.HOGWARTS);
            CreateCommunityMember(UserType.RON     , MemberType.HOGWARTS__RON            , CommunityType.HOGWARTS);
            CreateCommunityMember(UserType.GINNY   , MemberType.HOGWARTS__GINNY          , CommunityType.HOGWARTS);
            CreateCommunityMember(UserType.NEVILLE , MemberType.HOGWARTS__NEVILLE        , CommunityType.HOGWARTS);
            CreateCommunityMember(UserType.NEVILLE , MemberType.LOCKHART_SOCIETY__NEVILLE, CommunityType.LOCKHART_SOCIETY);
            CreateCommunityMember(UserType.MALFOY  , MemberType.HOGWARTS__MALFOY         , CommunityType.HOGWARTS);
        }

        private void CreateCommunityMember(UserType userType, MemberType memberType, CommunityType communityType)
        {
            var member = new CommunityMember();
            member.Id = Guid.NewGuid();
            member.Role = CommunityMember.MembershipRole.MEMBER;
            member.CommunityGroup = CommunityGroups[communityType];

            Users[userType].OwnedBooks.ForEach(b =>
            {
                member.PooledBooks.Add(new PooledBook
                {
                    Id = Guid.NewGuid(),
                    MemberBook = b
                });
            });

            CommunityMembers.Add(memberType, member);
        }
    }
}
