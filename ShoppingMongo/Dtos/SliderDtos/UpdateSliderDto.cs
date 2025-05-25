namespace ShoppingMongo.Dtos.SliderDtos
{
    public class UpdateSliderDto
    {
        public string SliderId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ExistingImagePath { get; set; } // Var olan resim yolu gösterilir
        public IFormFile? ImageFile { get; set; }  // Zorunlu değil, nullable olarak bırak
    }
}
