using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Services.CategoryServices;

namespace ShoppingMongo.Models.ViewComponents
{
    public class _DefaultCategoryComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _DefaultCategoryComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
    }
}
