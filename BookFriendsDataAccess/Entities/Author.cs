﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookFriendsDataAccess.Entities
{
    [Table("Author")]
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public String LastName { get; set; }
        [Required]
        [StringLength(100)]
        public String FirstNames { get; set; }
        public virtual ICollection<AuthorBook> Books { get; set; }
    }
}
