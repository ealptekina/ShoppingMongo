﻿using AutoMapper;
using MongoDB.Driver;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Entities;
using ShoppingMongo.Settings;

namespace ShoppingMongo.Services.CategoryServices
{

    public class CategoryService : ICategoryService
    {
        // AutoMapper nesnesi, DTO ve Entity sınıfları arasında dönüşüm işlemleri için kullanılır.
        private readonly IMapper _mapper;

        // MongoDB'deki "Categories" koleksiyonuna erişim sağlar.
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            // MongoDB istemcisi oluşturuluyor.
            var client = new MongoClient(_databaseSettings.ConnectionString);

            // Belirtilen veritabanı seçiliyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            // Veritabanındaki "Categories" koleksiyonu alınıyor.
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);

            // AutoMapper bağımlılığı atanıyor.
            _mapper = mapper;
        }


        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            // 1. DTO (CreateCategoryDto) nesnesini, gerçek veritabanı nesnesine (Category) dönüştürüyoruz.
            var value = _mapper.Map<Category>(createCategoryDto);

            // 2. MongoDB'deki "Categories" koleksiyonuna bu yeni değeri ekliyoruz (Insert işlemi).
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            // MongoDB koleksiyonunda CategoryId'si verilen id'ye eşit olan dökümanı bulur ve siler.
            // Silme işlemi asenkron olarak gerçekleştirilir.
            await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);
        }

        // Tüm kategorileri veritabanından asenkron olarak getirir ve DTO listesine dönüştürür
        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            // Kategori koleksiyonundaki tüm verileri getirir
            var values = await _categoryCollection.Find(x => true).ToListAsync();

            // Veritabanından alınan Category nesnelerini ResultCategoryDto türüne dönüştürerek döner
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        // Belirtilen ID'ye sahip kategoriyi asenkron olarak getirir
        public async Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id)
        {
            // Veritabanında CategoryId alanı verilen id ile eşleşen ilk kaydı bulur
            var value = await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();

            // Bulunan entity'yi DTO'ya dönüştürerek döndürür
            return _mapper.Map<GetCategoryByIdDto>(value);
        }


        public async Task<bool> UpdateCategoryAsync(GetCategoryByIdDto dto, IFormFile imageFile)
        {
            var category = await _categoryCollection.Find(x => x.CategoryId == dto.CategoryId).FirstOrDefaultAsync();
            if (category == null) return false;

            // Görsel varsa
            if (imageFile != null && imageFile.Length > 0)
            {
                var extension = Path.GetExtension(imageFile.FileName);
                var newImageName = $"{Guid.NewGuid()}{extension}";
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "categoryimages", newImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                category.ImagePath = "/categoryimages/" + newImageName;
            }

            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;

            var result = await _categoryCollection.ReplaceOneAsync(x => x.CategoryId == category.CategoryId, category);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }


        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.CategoryName, categoryName);
            return await _categoryCollection.Find(filter).FirstOrDefaultAsync();
        }

    }
}
