using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Accounts
{
	public class IndexModel : PageModel
	{
		private readonly BookStore.Models.BookStoreContext _context;

		public IndexModel(BookStore.Models.BookStoreContext context)
		{
			_context = context;
		}

		public IList<Account> Account { get; set; } = default!;
		public string Keywords { get; set; }

		public async Task OnGetAsync(string keyword)
		{
			Keywords = keyword;
			if (string.IsNullOrEmpty(keyword))
			{
				Account = await _context.Accounts
				.Include(a => a.Role).ToListAsync();
			}
			else
			{
				Account = await _context.Accounts
					.Include(a => a.Role)
					.Where(x => x.Username.Contains(keyword)
					|| x.Fullname.Contains(keyword)
					|| x.Role.RoleName.Contains(keyword))
					.OrderByDescending(x => x.AccountId)
					.ToListAsync();
			}
		}
	}
}
