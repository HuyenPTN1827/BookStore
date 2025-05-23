﻿using System;
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
		private readonly BookStore.Models.BookStoreContext _context;

		public CreateModel(BookStore.Models.BookStoreContext context)
		{
			_context = context;
		}

		public List<SubCategory> SubCategories { get; set; }

		public IActionResult OnGet()
		{
			ViewData["PublisherId"] = new SelectList(_context.Publishers, "PushlisherId", "PublisherName");
			SubCategories = _context.SubCategories.Include(x => x.Category).ToList();
			return Page();
		}

		[BindProperty]
		public Book Book { get; set; } = default!;


		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			//if (!ModelState.IsValid || _context.Books == null || Book == null)
			//{
			//	return Page();
			//}

			_context.Books.Add(Book);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
