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
				Books = context.Books
					.OrderByDescending(x => x.BookId)
					.ToList();
			}
			else
			{
				Books = context.Books
					.Where(x => x.BookName.Contains(keyword) 
					|| x.Author.Contains(keyword) 
					|| x.SubCategoryId == Convert.ToInt32(subCategoryId))
                    .OrderByDescending(x => x.BookId)
                    .ToList();
			}
		}
	}
}
