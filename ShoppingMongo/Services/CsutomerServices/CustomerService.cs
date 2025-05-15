using AutoMapper;
using MongoDB.Driver;
using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Entities;
using ShoppingMongo.Settings;

namespace ShoppingMongo.Services.CsutomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _customerCollection = database.GetCollection<Customer>(_databaseSettings.CustomerCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var value = _mapper.Map<Customer>(createCustomerDto);
            await _customerCollection.InsertOneAsync(value);
        }

        public async Task DeleteCustomerAsync(string customerId)
        {
            await _customerCollection.DeleteOneAsync(customerId);
        }

        public Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetCustomerByIdDto> GetCustomerByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            var value = _mapper.Map<Customer>(updateCustomerDto);
            await _customerCollection.FindOneAndReplaceAsync(
                x => x.CustomerId == updateCustomerDto.CustomerId,
                value);

        }
    }
}
