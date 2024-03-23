using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Books
{
    public class EditModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public EditModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BooksAuthor BooksAuthor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BooksAuthors == null)
            {
                return NotFound();
            }

            var booksauthor =  await _context.BooksAuthors.FirstOrDefaultAsync(m => m.BookId == id);
            if (booksauthor == null)
            {
                return NotFound();
            }
            BooksAuthor = booksauthor;
           ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId");
           ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BooksAuthor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksAuthorExists(BooksAuthor.BookId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BooksAuthorExists(int id)
        {
          return (_context.BooksAuthors?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
