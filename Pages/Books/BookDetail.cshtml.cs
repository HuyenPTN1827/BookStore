using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BookStore.Pages.Books
{
	public class BookDetailModel : PageModel
	{
		private readonly BookStoreContext context;

		public BookDetailModel()
		{
			context = new BookStoreContext();
		}

		public List<Category> Categories { get; set; }
		public List<SubCategory> SubCategories { get; set; }
		public List<Book> Books { get; set; }
		public Book Book { get; set; }
		public string Keywords { get; set; }

		public async Task<IActionResult> OnGetAsync(string? id)
		{
			Categories = context.Categories.ToList();
			SubCategories = context.SubCategories.ToList();

			if (id == null || context.Books == null)
			{
				return NotFound();
			}

			var book = await context.Books
				.Include(x => x.Publisher)
				.FirstOrDefaultAsync(x => x.BookId == Convert.ToInt32(id));

			if (book == null)
			{
				return NotFound();
			}
			else
			{
				Book = book;
			}
			return Page();
		}

		public IActionResult OnPost(string keyword)
		{
			Keywords = CleanInput(keyword);

			if (!string.IsNullOrEmpty(keyword))
			{
				return RedirectToPage("/Index", new { keyword = Keywords });
			}
			return Page();
		}

		// Hàm để loại bỏ các ký tự không hợp lệ
		private string CleanInput(string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			// Loại bỏ các ký tự không hợp lệ từ input
			string cleanedInput = new string(input.Where(c => IsValidInputChar(c)).ToArray());

			return cleanedInput;
		}

		// Kiểm tra xem ký tự có hợp lệ không
		private bool IsValidInputChar(char c)
		{
			// Thêm các điều kiện kiểm tra ký tự có hợp lệ ở đây
			// Trong trường hợp này, có thể bạn chỉ muốn loại bỏ ký tự không hợp lệ như ký tự đặc biệt hoặc dấu cách
			return !char.IsControl(c) && c != ' '; // Loại bỏ dấu cách và các ký tự điều khiển (control characters)
		}
	}
}
