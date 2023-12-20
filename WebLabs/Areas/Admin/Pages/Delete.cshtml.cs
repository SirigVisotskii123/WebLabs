using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebLabs.DAL.Data;
using WebLabs.DAL.Entities;

namespace WebLabs.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly WebLabs.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(WebLabs.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Dish Dish { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dish == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish.FirstOrDefaultAsync(m => m.DishId == id);

            if (dish == null)
            {
                return NotFound();
            }
            else 
            {
                Dish = dish;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Dish == null)
            {
                return NotFound();
            }
            var dish = await _context.Dish.FindAsync(id);

            if (dish != null)
            {
                Dish = dish;
                _context.Dish.Remove(Dish);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
