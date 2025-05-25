using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingMongo.Dtos.SliderDtos;
using ShoppingMongo.Entities;
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
            if (createSliderDto.ImageFile == null || createSliderDto.ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Lütfen bir görsel seçiniz.");
                return View(createSliderDto);
            }

            // Görseli sunucuya kaydet
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(createSliderDto.ImageFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/sliderimages", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await createSliderDto.ImageFile.CopyToAsync(stream);
            }

            createSliderDto.Image = fileName; // DTO'nun Image alanı MongoDB'ye gidecek değer

            await _sliderservice.CreateSliderAsync(createSliderDto);

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
            var value = await _sliderservice.GetSliderByIdAsync(id);

            var dto = new UpdateSliderDto
            {
                SliderId = value.SliderId,
                Title = value.Title,
                SubTitle = value.SubTitle,
                ExistingImagePath = value.Image  // burayı Image olarak değiştir
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var filter = Builders<Slider>.Filter.Eq(s => s.SliderId, model.SliderId);
            var slider = await _sliderservice.GetSliderByIdAsync(model.SliderId);

            if (slider == null)
            {
                return NotFound();
            }

            // Güncellenebilir alanları güncelle
            slider.Title = model.Title;
            slider.SubTitle = model.SubTitle;

            // Yeni dosya yüklenmişse işle
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // Eski resmi sil
                if (!string.IsNullOrEmpty(slider.Image))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/sliderimages", slider.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni resmi kaydet
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/sliderimages", newFileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                slider.Image = newFileName;
            }

            // MongoDB'de güncelleme
            var update = Builders<Slider>.Update
                .Set(s => s.Title, slider.Title)
                .Set(s => s.SubTitle, slider.SubTitle)
                .Set(s => s.Image, slider.Image);

            var result = await _sliderservice.UpdateSliderAsync(model.SliderId, update);

            if (!result)
            {
                ModelState.AddModelError("", "Güncelleme sırasında hata oluştu.");
                return View(model);
            }

            return RedirectToAction("SliderList");
        }
    }
}
