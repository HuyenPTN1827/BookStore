using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Book
    {
        public Book()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BookId { get; set; }
        public string BookName { get; set; } = null!;
        public string? Cover { get; set; }
        public string Author { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int PublisherId { get; set; }
        public int SubCategoryId { get; set; }

        public virtual Publisher Publisher { get; set; } = null!;
        public virtual SubCategory SubCategory { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
