using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Books
{
    public class DetailsModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public DetailsModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

      public BooksAuthor BooksAuthor { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BooksAuthors == null)
            {
                return NotFound();
            }

            var booksauthor = await _context.BooksAuthors.FirstOrDefaultAsync(m => m.BookId == id);
            if (booksauthor == null)
            {
                return NotFound();
            }
            else 
            {
                BooksAuthor = booksauthor;
            }
            return Page();
        }
    }
}
