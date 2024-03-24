using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Books
{
	public class IndexModel : PageModel
	{
		private readonly BookStoreContext context;

		public IndexModel()
		{
			context = new BookStoreContext();
		}

		public List<SubCategory> SubCategories { get; set; }
		public List<Book> Books { get; set; }
		public string Keywords { get; set; }

		public void OnGet(string keyword)
		{
			Keywords = keyword;
			SubCategories = context.SubCategories.ToList();

			if (string.IsNullOrEmpty(keyword))
			{
				Books = context.Books
					.Include(x => x.Publisher)
					.OrderByDescending(x => x.BookId)
					.ToList();
			}
			else
			{
				Books = context.Books
					.Include(x => x.Publisher)
					.Where(x => x.BookName.Contains(keyword))
					.OrderByDescending(x => x.BookId)
					.ToList();
			}
		}
	}
}
