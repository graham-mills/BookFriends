using BookFriends.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.Data
{
    public class BookFriendsDbContext : DbContext
    {
        public BookFriendsDbContext(DbContextOptions<BookFriendsDbContext> options) : base(options)
        {
        }

        public DbSet<CommunityGroup> CommunityGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CommunityMember> CommunityMembers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<OwnedBook> OwnedBooks { get; set; }
        public DbSet<PooledBook> PooledBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Override default pluralisation of tables
            modelBuilder.Entity<CommunityGroup>().ToTable("CommunityGroup");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<CommunityMember>().ToTable("CommunityMember");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<OwnedBook>().ToTable("OwnedBook");
            modelBuilder.Entity<PooledBook>().ToTable("PooledBook");


        }

    }
}
