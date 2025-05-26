using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ShoppingMongo.Entities;
using ShoppingMongo.Services.SliderService;

namespace ShoppingMongo.Models.ViewComponents
{
    public class _DefaultSliderComponentPartial:ViewComponent
    {
        private readonly IMongoCollection<Slider> _sliderCollection;

        public _DefaultSliderComponentPartial(IMongoClient client)
        {
            var database = client.GetDatabase("ShoppingDb");
            _sliderCollection = database.GetCollection<Slider>("Sliders");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _sliderCollection.Find(_ => true).ToListAsync();
            return View(sliders);
        }
    }
}
