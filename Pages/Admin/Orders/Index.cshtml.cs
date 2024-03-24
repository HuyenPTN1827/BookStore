using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public IndexModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;
		public string Keywords { get; set; }
		public async Task OnGetAsync(string keyword)
        {
			Keywords = keyword;
			if (string.IsNullOrEmpty(keyword))
			{
				Order = await _context.Orders
                .Include(o => o.Account)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            }
            else
            {
				Order = await _context.Orders
				.Include(o => o.Account)
                .Where(o => o.Status.Contains(keyword)
                || o.Account.Fullname.Contains(keyword))
				.OrderByDescending(o => o.OrderDate)
				.ToListAsync();
			}
        }
    }
}
