using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models;

namespace BookStore.Pages.Admin.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public CreateModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId");
        ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            return Page();
        }

        [BindProperty]
        public BooksAuthor BooksAuthor { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BooksAuthors == null || BooksAuthor == null)
            {
                return Page();
            }

            _context.BooksAuthors.Add(BooksAuthor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
