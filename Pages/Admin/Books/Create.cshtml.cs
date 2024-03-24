using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Pages.Admin.Books
{
	public class CreateModel : PageModel
	{
		private readonly BookStoreContext context;

		public CreateModel()
		{
			context = new BookStoreContext();
		}

		public List<Category> Categories { get; set; }
		public List<SubCategory> SubCategories { get; set; }
		public List<Author> Authors { get; set; }
		public List<Publisher> Publishers { get; set; }
		public List<BooksAuthor> Books_Authors { get; set; }
		public Book Book { get; set; }
		public Author Author { get; set; }
		public BooksAuthor BooksAuthor { get; set; }

		public string BookName { get; set; }
		public string Cover { get; set; }
		public string AuthorName { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public int PublisherId { get; set; }
		public int SubCategoryId { get; set; }
		public string Description { get; set; }

		public IActionResult OnGet()
		{
			Categories = context.Categories.ToList();
			SubCategories = context.SubCategories.ToList();
			Publishers = context.Publishers.ToList();
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string bookName, string cover, string authorName, decimal price, 
			int quantity, int publisher, int subCategory, string description)
		{
			BookName = bookName;
			Cover = cover;
			AuthorName = authorName;
			Price = price;
			Quantity = quantity;
			PublisherId = publisher;
			SubCategoryId = subCategory;
			Description = description;

			Categories = await context.Categories.ToListAsync();
			SubCategories = await context.SubCategories.ToListAsync();
			Publishers = await context.Publishers.ToListAsync();

			var books = Book;
			var authors = Author;
			var booksAuthor = BooksAuthor;

			//if (!ModelState.IsValid)
			//{
			//	return Page();
			//}

			books.BookName = bookName;
			books.Price = price;
			books.Quantity = quantity;
			books.PublisherId = publisher;
			books.SubCategoryId = subCategory;
			books.Description = description;
			context.Books.Add(books);
			await context.SaveChangesAsync();

			authors.AuthorName = authorName;
			context.Authors.Add(authors);
			await context.SaveChangesAsync();

			var bookId = await context.Books.FirstOrDefaultAsync(x => x.BookName.Equals(bookName));
			var authorId = await context.Authors.FirstOrDefaultAsync(x => x.AuthorName.Equals(authorName));

			booksAuthor.BookId = bookId.BookId;
			booksAuthor.AuthorId = authorId.AuthorId;
			booksAuthor.Position = "Writer";
			context.BooksAuthors.Add(booksAuthor);
			await context.SaveChangesAsync();

			return Redirect("/Admin/Books");
		}
	}
}
