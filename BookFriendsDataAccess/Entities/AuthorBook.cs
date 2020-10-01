using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookFriendsDataAccess.Entities
{
    [Table("AuthorBook")]
    public class AuthorBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}
