using Microsoft.AspNetCore.Mvc;

namespace ShoppingMongo.Controllers
{
    public class AdminLayoutController : Controller
    {
        // Admin paneli ana sayfasını döndüren aksiyon
        public IActionResult Index()
        {
            return View();
        }

        // Sayfanın <head> bölümüne ait partial view'i döndüren aksiyon
        public PartialViewResult PartialHead()
        {
            return PartialView();
        }

        // Admin panelinin yan menüsünü döndüren aksiyon
        public PartialViewResult PartialSidebar()
        {
            return PartialView();
        }
        public PartialViewResult PartialScript()
        {
            return PartialView();
        }
    }
}
