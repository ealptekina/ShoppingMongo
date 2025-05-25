using Microsoft.AspNetCore.Mvc;
using ShoppingMongo.Dtos.SliderDtos;
using ShoppingMongo.Services.SliderService;

namespace ShoppingMongo.Controllers
{
    public class SlidersController : Controller
    {

        private readonly ISliderService _sliderservice;

        public SlidersController(ISliderService sliderservice)
        {
            _sliderservice = sliderservice;
        }

        public async Task<IActionResult> SliderList()
        {
            var values = await _sliderservice.GetAllSliderAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderDto createSliderDto)
        {
            // Formdan gelen veriyi veritabanına kaydetmek için servis katmanını çağır
            await _sliderservice.CreateSliderAsync(createSliderDto);
            // Başarılı işlem sonrası ürünler listesine yönlendir
            return RedirectToAction("SliderList");
        }

        public async Task<IActionResult> DeleteSlider(string id)
        {
            await _sliderservice.DeleteSliderAsync(id);
            return RedirectToAction("SliderList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSlider(string id)
        {
            // Güncellenecek ürün verisi ID ile alınıyor
            var value = await _sliderservice.GetSliderByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateSliderDto updateSliderDto)
        {
            await _sliderservice.UpdateSliderAsync(updateSliderDto);
            return RedirectToAction("SliderList");
        }
    }
}
