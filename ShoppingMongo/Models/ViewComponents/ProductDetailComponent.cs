using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Services.ProductServices;
using ShoppingMongo.Dtos.ProductDos;

namespace ShoppingMongo.ViewComponents
{
    public class ProductDetailComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductDetailComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return Content("Ürün bulunamadı.");
            }

            var dto = new GetProductByIdDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                Status = product.Status,
                StockCount = product.StockCount,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                Size = product.Size,
                Color = product.Color,
                ImageUrls = product.ImageUrls ?? new List<string>(),
                ImagePaths = product.ImagePaths ?? new List<string>()
            };

            return View(dto);
        }
    }
}
