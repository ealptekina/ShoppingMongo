using AutoMapper;
using Humanizer;
using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingMongo.Dtos.ProductDos;
using ShoppingMongo.Dtos.SliderDtos;
using ShoppingMongo.Entities;
using ShoppingMongo.Settings;

namespace ShoppingMongo.Services.SliderService
{
   
    public class SliderService : ISliderService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Slider> _sliderCollection;

        public SliderService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _sliderCollection = database.GetCollection<Slider>(_databaseSettings.SliderCollectionName);
            _mapper = mapper;
        }

       

        public async Task CreateSliderAsync(CreateSliderDto createSliderDto)
        {
            var value = _mapper.Map<Slider>(createSliderDto);
            await _sliderCollection.InsertOneAsync(value);
        }

        public async Task DeleteSliderAsync(string id)
        {
            await _sliderCollection.DeleteOneAsync(x => x.SliderId == id);
        }

        public async Task<List<ResultSliderDto>> GetAllSliderAsync()
        {
            var values = await _sliderCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultSliderDto>>(values);
        }

        public async Task<GetSliderByIdDto> GetSliderByIdAsync(string id)
        {
            var value = await _sliderCollection.Find(x => x.SliderId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetSliderByIdDto>(value);
        }

        public async Task<bool> UpdateSliderAsync(string sliderId, UpdateDefinition<Slider> update)
        {
            var filter = Builders<Slider>.Filter.Eq(s => s.SliderId, sliderId);
            var result = await _sliderCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public List<Slider> GetSliders()
        {
            return _sliderCollection.Find(_ => true).ToList();
        }

    }
}
