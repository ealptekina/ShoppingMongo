using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Services.CategoryServices;
using System.Threading.Tasks;

namespace ShoppingMongo.Controllers
{
    public class CategoriesController : Controller
    {
        // Constructor: Controller'a bağımlılık olarak ICategoryService enjekte ediliyor.
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Kategori listesini getirip CategoryList View'ına gönderir.
        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryService.GetAllCategoryAsync(); // Tüm kategorileri al
            return View(values); // View'a model olarak gönder
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            // Yeni kategori oluşturma formunu kullanıcıya gösterir.
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            // Formdan gelen veriyi veritabanına kaydetmek için servis katmanını çağır
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            // Başarılı işlem sonrası kategori listesine yönlendir
            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            // Güncellenecek kategori verisi ID ile alınıyor
            var value = await _categoryService.GetCategoryByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            return RedirectToAction("CategoryList");
        }

    }
}
