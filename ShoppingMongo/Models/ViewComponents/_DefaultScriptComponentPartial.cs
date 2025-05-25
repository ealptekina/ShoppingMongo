using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.Models.ViewComponents
{
    public class _DefaultScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
