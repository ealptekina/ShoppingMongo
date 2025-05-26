using AutoMapper;
using MongoDB.Driver;
using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Dtos.ProductDos;
using ShoppingMongo.Entities;
using ShoppingMongo.Services.CategoryServices;
using ShoppingMongo.Settings;

namespace ShoppingMongo.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productColletion;
        private readonly ICategoryService _categoryService;


        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings, ICategoryService categoryService)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productColletion = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _mapper = mapper;
            _categoryService = categoryService;
        }


        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            await _productColletion.InsertOneAsync(value);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productColletion.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = await _productColletion.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<GetProductByIdDto> GetProductByIdAsync(string id)
        {
            var value = await _productColletion.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetProductByIdDto>(value);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            await _productColletion.FindOneAndReplaceAsync(
                x => x.ProductId == updateProductDto.ProductId,
                value);
        }

        public async Task<List<ResultProductDto>> GetProductsByCategoryNameAsync(string categoryName)
        {
            var category = await _categoryService.GetCategoryByNameAsync(categoryName);
            if (category == null)
                return new List<ResultProductDto>();

            var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, category.CategoryId);
            var products = await _productColletion.Find(filter).ToListAsync();

            return _mapper.Map<List<ResultProductDto>>(products);
        }
        public async Task<List<ResultProductDto>> GetAllAsync()
        {
            var values = await _productColletion.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }
    }
}
