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
	public class DetailsModel : PageModel
	{
		private readonly BookStoreContext context;

		public DetailsModel()
		{
			context = new BookStoreContext();
		}

		public List<Category> Categories { get; set; }
		public List<SubCategory> SubCategories { get; set; }
		public List<Book> Books { get; set; }
		public List<Author> Authors { get; set; }
		public BooksAuthor Books_Authors { get; set; }

		public async Task<IActionResult> OnGetAsync(string? id)
		{
			Categories = context.Categories.ToList();
			SubCategories = context.SubCategories.ToList();

			if (id == null || context.BooksAuthors == null)
			{
				return NotFound();
			}

			var book = await context.BooksAuthors
				.Include(x => x.Book)
				.Include(x => x.Author)
				.Include(x => x.Book.Publisher)
				.FirstOrDefaultAsync(x => x.BookId == Convert.ToInt32(id));

			if (book == null)
			{
				return NotFound();
			}
			else
			{
				Books_Authors = book;
			}
			return Page();
		}
	}
}
