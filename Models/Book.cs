using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Book
    {
        public Book()
        {
            BooksAuthors = new HashSet<BooksAuthor>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BookId { get; set; }
        public string BookName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Cover { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? PublisherId { get; set; }
        public int? SubCategoryId { get; set; }

        public virtual Publisher? Publisher { get; set; }
        public virtual SubCategory? SubCategory { get; set; }
        public virtual ICollection<BooksAuthor> BooksAuthors { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
