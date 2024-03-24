using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BookStore.Pages.Admin.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public IndexModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get;set; } = default!;
		public string Keywords { get; set; }

		public async Task OnGetAsync(string keyword)
        {
			Keywords = keyword;

			if (string.IsNullOrEmpty(keyword))
			{
				Publisher = await _context.Publishers
                    .OrderByDescending(x => x.PushlisherId)
                    .ToListAsync();
            }
            else
            {
				Publisher = await _context.Publishers
                    .Where(x => x.PublisherName.Contains(keyword))
					.OrderByDescending(x => x.PushlisherId)
					.ToListAsync();
			}
        }
    }
}
