using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.CustomerDtos;
using ShoppingMongo.Dtos.ProductDos;
using ShoppingMongo.Services.CsutomerServices;

namespace ShoppingMongo.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> CustomerList()
        {
            var values = await _customerService.GetAllCustomerAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            // Formdan gelen veriyi veritabanına kaydetmek için servis katmanını çağır
            await _customerService.CreateCustomerAsync(createCustomerDto);
            // Başarılı işlem sonrası müşteriler listesine yönlendir
            return RedirectToAction("CustomerList");
        }

        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("CustomerList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            // Güncellenecek müşteri verisi ID ile alınıyor
            var value = await _customerService.GetCustomerByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            await _customerService.UpdateCustomerAsync(updateCustomerDto);
            return RedirectToAction("CustomerList");
        }
    }
}
