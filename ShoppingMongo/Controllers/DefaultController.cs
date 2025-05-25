using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
