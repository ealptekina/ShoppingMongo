using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Services.CategoryServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using MongoDB.Driver;
using ShoppingMongo.Entities;

namespace ShoppingMongo.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Kategori listesini getirir
        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Klasör yolunu belirle
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "categoryimages");

                // Eğer klasör yoksa oluştur
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // Dosya ismini benzersiz yap (örneğin Guid + orijinal uzantı)
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                // Dosyayı kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // DTO’nun ImagePath’ine kaydedilen yol atanır (web’den erişim için relative path)
                createCategoryDto.ImagePath = "/categoryimages/" + fileName;
            }

            await _categoryService.CreateCategoryAsync(createCategoryDto);
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
            var value = await _categoryService.GetCategoryByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(GetCategoryByIdDto dto, IFormFile imageFile)
        {
            // MongoDB bağlantısı oluştur
            var client = new MongoClient("mongodb://localhost:27017"); // Gerekirse config'ten çek
            var database = client.GetDatabase("ShoppingDb"); // Veritabanı adı doğru olmalı
            var categoryCollection = database.GetCollection<Category>("Categories"); // Koleksiyon adı doğru olmalı

            var category = await categoryCollection.Find(x => x.CategoryId == dto.CategoryId).FirstOrDefaultAsync();
            if (category == null)
            {
                return NotFound("Kategori bulunamadı");
            }

            // Görsel yükleme varsa
            if (imageFile != null && imageFile.Length > 0)
            {
                var extension = Path.GetExtension(imageFile.FileName);
                var newImageName = $"{Guid.NewGuid()}{extension}";
                var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "categoryimages");

                if (!Directory.Exists(imageFolder))
                    Directory.CreateDirectory(imageFolder);

                var imagePath = Path.Combine(imageFolder, newImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                category.ImagePath = "/categoryimages/" + newImageName;
            }

            // Diğer alanları güncelle
            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;

            var result = await categoryCollection.ReplaceOneAsync(x => x.CategoryId == category.CategoryId, category);

            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                return RedirectToAction("CategoryList");
            }

            ViewBag.Error = "Güncelleme başarısız oldu.";
            return View(dto);
        }


    }
}
