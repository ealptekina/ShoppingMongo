using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Dtos.ProductDos;
using ShoppingMongo.Services.CategoryServices;
using ShoppingMongo.Services.ProductServices;
using System.Threading.Tasks;

namespace ShoppingMongo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productservice;

        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productservice, ICategoryService categoryService)
        {
            _productservice = productservice;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ProductList()
        {
            var values = await _productservice.GetAllProductAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewBag.v = categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId
            }).ToList();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            // Formdan gelen veriyi veritabanına kaydetmek için servis katmanını çağır
            await _productservice.CreateProductAsync(createProductDto);
            // Başarılı işlem sonrası ürünler listesine yönlendir
            return RedirectToAction("ProductList");
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productservice.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            // Güncellenecek ürün verisi ID ile alınıyor
            var value = await _productservice.GetProductByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateProductDto updateProductDto)
        {
            await _productservice.UpdateProductAsync(updateProductDto);
            return RedirectToAction("ProductList");
        }
    }
}
