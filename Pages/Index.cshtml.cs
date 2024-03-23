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
		public string SubCategoryId { get; set; }

		public void OnGet(string keyword, string subCategoryId)
		{
			Keywords = keyword;
			SubCategoryId = subCategoryId;

			Categories = context.Categories.ToList();
			SubCategories = context.SubCategories.ToList();

			if (string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(subCategoryId))
			{
				BooksAuthors = context.BooksAuthors
					.Include(x => x.Book)
					.Where(x => x.Position.Equals("Writer"))
					.OrderByDescending(x => x.BookId)
					.ToList();
			}
			else
			{
				BooksAuthors = context.BooksAuthors
					.Include(x => x.Book)
					.Include(x => x.Author)
					.Where(x => (x.Position.Equals("Writer") && x.Book.BookName.Contains(keyword))
					|| (x.Position.Equals("Writer") && x.Author.AuthorName.Contains(keyword))
					|| (x.Position.Equals("Writer") && x.Book.SubCategoryId == Convert.ToInt32(subCategoryId)))
                    .OrderByDescending(x => x.BookId)
                    .ToList();
			}
		}
	}
}
