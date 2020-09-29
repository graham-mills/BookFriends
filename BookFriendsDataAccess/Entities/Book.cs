using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Series { get; set; }
        public String Publisher { get; set; }
        public List<Author> Authors { get; set; }

        public Book()
        {
            Authors = new List<Author>();
        }
    }
}
