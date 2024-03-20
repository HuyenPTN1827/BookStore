using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Books = new HashSet<Book>();
        }

        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; }
    }
}
