using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebLabs.DAL.Data;
using WebLabs.DAL.Entities;

namespace WebLabs.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WebLabs.DAL.Data.ApplicationDbContext _context;

        public CreateModel(WebLabs.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DishGroupId"] = new SelectList(_context.DishGroup, "DishGroupId", "GroupName");
            return Page();
        }

        [BindProperty]
        public Dish Dish { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Dish == null || Dish == null)
            {
                return Page();
            }

            _context.Dish.Add(Dish);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
