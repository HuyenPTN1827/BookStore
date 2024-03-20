using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Author
    {
        public Author()
        {
            BooksAuthors = new HashSet<BooksAuthor>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Biography { get; set; }

        public virtual ICollection<BooksAuthor> BooksAuthors { get; set; }
    }
}
