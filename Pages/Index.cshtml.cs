using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Pages
{
	public class IndexModel : PageModel
	{
		private readonly BookStoreContext context;

		public IndexModel()
		{
			context = new BookStoreContext();
		}

		public List<Category> Categories { get; set; }
		public List<SubCategory> SubCategories { get; set; }
		public List<Book> Books { get; set; }
		public List<Author> Authors { get; set; }
		public List<BooksAuthor> BooksAuthors { get; set; } 
		public string Keywords { get; set; }

		public void OnGet(string keyword)
		{
			Keywords = keyword;
			Categories = context.Categories.ToList();
			SubCategories = context.SubCategories.ToList();

			if (string.IsNullOrEmpty(keyword))
			{
				BooksAuthors = context.BooksAuthors
					.Include(x => x.Book)
					.Where(x => x.Position.Equals("Author"))
					.ToList();
			}
			else
			{
				BooksAuthors = context.BooksAuthors
					.Include(x => x.Book)
					.Include(x => x.Author)
					.Where(x => (x.Position.Equals("Author") && x.Book.BookName.Contains(keyword)) 
					|| (x.Position.Equals("Author") && x.Author.AuthorName.Contains(keyword)))
					.ToList();
			}
		}
	}
}
