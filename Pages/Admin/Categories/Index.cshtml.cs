using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public IndexModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
		public string Keywords { get; set; }

		public async Task OnGetAsync(string keyword)
        {
			Keywords = keyword;

			if (string.IsNullOrEmpty(keyword))
			{
				Category = await _context.Categories
					.OrderByDescending(x => x.CategoryId)
					.ToListAsync();
            }
            else
            {
				Category = await _context.Categories
                    .Where(x => x.CategoryName.Contains(keyword))
                    .OrderByDescending(x => x.CategoryId)
                    .ToListAsync();
			}
        }
    }
}
