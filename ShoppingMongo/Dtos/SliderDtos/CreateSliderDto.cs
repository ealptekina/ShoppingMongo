namespace ShoppingMongo.Dtos.SliderDtos
{
    public class CreateSliderDto
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public IFormFile ImageFile { get; set; }  // Dosya buraya yüklenecek
        public string Image { get; set; } // Bu MongoDB'ye gidecek görsel adı
    }
}
