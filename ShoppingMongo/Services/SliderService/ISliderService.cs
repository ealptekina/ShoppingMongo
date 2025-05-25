using MongoDB.Driver;
using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Dtos.SliderDtos;
using ShoppingMongo.Entities;

namespace ShoppingMongo.Services.SliderService
{
    public interface ISliderService
    {
        Task<List<ResultSliderDto>> GetAllSliderAsync();
        Task CreateSliderAsync(CreateSliderDto createSliderDto);
        Task<bool> UpdateSliderAsync(string sliderId, UpdateDefinition<Slider> update);
        Task DeleteSliderAsync(string id);
        Task<GetSliderByIdDto> GetSliderByIdAsync(string id);
    }
}
