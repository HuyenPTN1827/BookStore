﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Books
{
    public class DeleteModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public DeleteModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;
        public List<Publisher> Publishers { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(x => x.Publisher)
                .Include(x => x.SubCategory)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                Book = book;
                _context.Books.Remove(Book);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
