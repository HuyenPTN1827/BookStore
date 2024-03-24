using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models;

namespace BookStore.Pages.Admin.SubCategories
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return Page();
        }

        [BindProperty]
        public SubCategory SubCategory { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.SubCategories == null || SubCategory == null)
            {
                return Page();
            }

            _context.SubCategories.Add(SubCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
