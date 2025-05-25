using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Dtos.SliderDtos;

namespace ShoppingMongo.Services.SliderService
{
    public interface ISliderService
    {
        Task<List<ResultSliderDto>> GetAllSliderAsync();
        Task CreateSliderAsync(CreateSliderDto createSliderDto);
        Task UpdateSliderAsync(UpdateSliderDto updateSliderDto);
        Task DeleteSliderAsync(string id);
        Task<GetSliderByIdDto> GetSliderByIdAsync(string id);
    }
}
