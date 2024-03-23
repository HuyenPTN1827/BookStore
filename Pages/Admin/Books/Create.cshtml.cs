using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Pages.Admin.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookStoreContext context;

        public CreateModel()
        {
            context = new BookStoreContext();
        }

        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<BooksAuthor> Books_Authors { get; set; }
        public Book Book { get; set; }
        public Author Author { get; set; }
        public BooksAuthor BooksAuthors { get; set; }

        public IActionResult OnGet()
        {
            Categories = context.Categories.ToList(); 
            SubCategories = context.SubCategories.ToList();
            Publishers = context.Publishers.ToList();
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || _context.BooksAuthors == null || BooksAuthor == null)
            //  {
            //      return Page();
            //  }

            //  _context.BooksAuthors.Add(BooksAuthor);
            //  await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
