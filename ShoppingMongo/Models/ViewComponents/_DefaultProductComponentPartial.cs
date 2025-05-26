using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.ProductDos;
using ShoppingMongo.Services.CategoryServices;
using ShoppingMongo.Services.ProductServices;

public class _DefaultProductComponentPartial : ViewComponent
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public _DefaultProductComponentPartial(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _productService.GetAllAsync();
        var categories = await _categoryService.GetAllAsync();

        var productDtos = products.Select(p =>
        {
            var categoryName = categories.FirstOrDefault(c => c.CategoryId == p.CategoryId)?.CategoryName ?? "Uncategorized";

            return new GetProductByIdDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                ImageUrls = p.ImageUrls,
                Status = p.Status,
                StockCount = p.StockCount,
                CategoryId = p.CategoryId,
                CategoryName = categoryName,
                Size = p.Size,
                Color = p.Color
            };
        }).ToList();

        return View(productDtos);
    }
}
