using Microsoft.AspNetCore.Mvc;

namespace WebLabs.Components
{
    public class CartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
