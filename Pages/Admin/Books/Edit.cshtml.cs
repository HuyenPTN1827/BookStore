﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Books
{
	public class EditModel : PageModel
	{
		private readonly BookStore.Models.BookStoreContext _context;

		public EditModel(BookStore.Models.BookStoreContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Book Book { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Books == null)
			{
				return NotFound();
			}

			var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
			if (book == null)
			{
				return NotFound();
			}
			Book = book;
			ViewData["PublisherId"] = new SelectList(_context.Publishers, "PushlisherId", "PublisherName");
			ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubCategoryId", "SubCategoryName");
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			//if (!ModelState.IsValid)
			//{
			//    return Page();
			//}

			_context.Attach(Book).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BookExists(Book.BookId))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool BookExists(int id)
		{
			return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
		}
	}
}
