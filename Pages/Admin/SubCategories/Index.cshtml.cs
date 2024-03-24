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
    public class IndexModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public IndexModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        public IList<SubCategory> SubCategory { get;set; } = default!;
		public string Keywords { get; set; }

		public async Task OnGetAsync(string keyword)
        {
			Keywords = keyword;

			if (string.IsNullOrEmpty(keyword))
			{
				SubCategory = await _context.SubCategories
                .Include(s => s.Category)
                .OrderByDescending(s => s.SubCategoryId)
                .ToListAsync();
            }
            else
            {
				SubCategory = await _context.SubCategories
				.Include(s => s.Category)
                .Where(s => s.SubCategoryName.Contains(keyword)
                || s.Category.CategoryName.Contains(keyword))
				.OrderByDescending(s => s.SubCategoryId)
				.ToListAsync();
			}
        }
    }
}
