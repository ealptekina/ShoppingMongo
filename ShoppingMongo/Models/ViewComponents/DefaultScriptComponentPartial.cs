using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.Models.ViewComponents
{
    public class DefaultScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
