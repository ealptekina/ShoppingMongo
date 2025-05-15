using AutoMapper;
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
            await _categoryCollection.DeleteOneAsync(id);
        }

        public Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            // Güncellenmek istenen veriyi taşıyan DTO nesnesini Category entity'sine dönüştürüyoruz.
            // Bu dönüşüm AutoMapper ile yapılır.
            var value = _mapper.Map<Category>(updateCategoryDto);

            // MongoDB'de CategoryId'si DTO içindeki CategoryId ile eşleşen dökümanı bulur
            // ve yerine bu yeni Category nesnesini koyar (güncelleme işlemi).
            await _categoryCollection.FindOneAndReplaceAsync(
                x => x.CategoryId == updateCategoryDto.CategoryId,
                value);
        }
    }
}
