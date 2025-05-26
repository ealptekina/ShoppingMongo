using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Dtos.ProductDos;

namespace ShoppingMongo.Services.CsutomerServices
{
    public interface ICustomerService
    {
        Task<List<ResultCustomerDto>> GetAllCustomerAsync();
        Task CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto);
        Task DeleteCustomerAsync(string id);
        Task<GetCustomerByIdDto> GetCustomerByIdAsync(string id);
    }
}
