using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Services.ProductServices;

namespace ShoppingMongo.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IProductService _productService;

        public DefaultController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DetailComponent(string id)
        {
            return View(model: id);
        }
    }
}
