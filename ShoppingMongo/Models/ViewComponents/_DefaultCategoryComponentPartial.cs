using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.Models.ViewComponents
{
    public class _DefaultCategoryComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
