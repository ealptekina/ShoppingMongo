using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.Models.ViewComponents
{
    public class _DefaultSliderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
