using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLabs.DAL.Data;
using WebLabs.Extensions;
using WebLabs.Models;

namespace WebLabs.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private string cartKey = "cart";
        private ICart _cart;
        public CartController(ApplicationDbContext context, ICart cart)
        {
            _context = context;
            _cart = cart;
        }
        public IActionResult Index()
        {
             _cart = HttpContext.Session.Get<Cart>(cartKey);
            return View(_cart.Items.Values);
            //return View();
        }

        [Authorize]
        public IActionResult Add(int id, string returnUrl)
        {
            _cart = HttpContext.Session.Get<Cart>(cartKey);
            var item = _context.Dish.Find(id);
            if (item != null)
            {
                _cart.AddToCart(item);
                HttpContext.Session.Set<Cart>(cartKey, (Cart)_cart);
            }
            return Redirect(returnUrl);
        }

		public IActionResult Delete(int id)
		{
			_cart.RemoveFromCart(id);
			return RedirectToAction("Index");
		}

	}
}
