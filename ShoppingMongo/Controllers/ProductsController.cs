using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Dtos.ProductDos;
using ShoppingMongo.Services.ProductServices;

namespace ShoppingMongo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productservice;

        public ProductsController(IProductService productservice)
        {
            _productservice = productservice;
        }

        public async Task<IActionResult> ProductList()
        {
            var values = await _productservice.GetAllProductAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
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
