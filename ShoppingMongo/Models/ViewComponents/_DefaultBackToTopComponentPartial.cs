using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.Models.ViewComponents
{
    public class _DefaultBackToTopComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
