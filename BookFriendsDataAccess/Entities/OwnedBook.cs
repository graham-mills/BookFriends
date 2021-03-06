﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    [Table("OwnedBook")]
    public class OwnedBook
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        public Boolean Available { get; set; }
        public String Notes { get; set; }
    }
}
