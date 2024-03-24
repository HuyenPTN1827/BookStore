using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.SubCategories
{
    public class DeleteModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public DeleteModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
      public SubCategory SubCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            var subcategory = await _context.SubCategories
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.SubCategoryId == id);

            if (subcategory == null)
            {
                return NotFound();
            }
            else 
            {
                SubCategory = subcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }
            var subcategory = await _context.SubCategories.FindAsync(id);

            if (subcategory != null)
            {
                SubCategory = subcategory;
                _context.SubCategories.Remove(SubCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
