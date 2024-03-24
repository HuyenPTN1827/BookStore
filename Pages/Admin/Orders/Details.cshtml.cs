using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Pages.Admin.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly BookStore.Models.BookStoreContext _context;

        public DetailsModel(BookStore.Models.BookStoreContext context)
        {
            _context = context;
        }

        public OrderDetail OrderDetail { get; set; }
        //public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(m => m.Order)
                .Include(m => m.Book)
                .Include(m => m.Order.Account)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetail = orderDetail;
            }
            return Page();
        }
    }
}
