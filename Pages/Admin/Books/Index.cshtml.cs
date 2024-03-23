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
		public List<Author> Authors { get; set; }
		public List<BooksAuthor> BooksAuthors { get; set; }
		public string Keywords { get; set; }

		public void OnGet(string keyword)
		{
			Keywords = keyword;
			SubCategories = context.SubCategories.ToList();

			if (string.IsNullOrEmpty(keyword))
			{
				BooksAuthors = context.BooksAuthors
					.Include(x => x.Book)
					.Include(x => x.Author)
					.Include(x => x.Book.Publisher)
					.Where(x => x.Position.Equals("Writer"))
                    .OrderByDescending(x => x.BookId)
                    .ToList();
			}
			else
			{
				BooksAuthors = context.BooksAuthors
					.Include(x => x.Book)
					.Include(x => x.Author)
					.Include(x => x.Book.Publisher)
					.Where(x => x.Position.Equals("Author") && x.Book.BookName.Contains(keyword))
                    .OrderByDescending(x => x.BookId)
                    .ToList();
			}
		}
	}
}
