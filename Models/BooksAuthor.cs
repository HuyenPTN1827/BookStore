using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class BooksAuthor
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public string? Position { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Book Book { get; set; } = null!;
    }
}
