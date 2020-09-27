using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.Data.Entities
{
    public class OwnedBook
    {
        [Key]
        public Guid Id { get; set; }
        public Book Book { get; set; }
        public Boolean Available { get; set; }
        public String Notes { get; set; }
    }
}
