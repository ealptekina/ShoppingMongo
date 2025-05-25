namespace ShoppingMongo.Dtos.CategoryDtos
{
    public class GetCategoryByIdDto
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }  // Eğer varsa, eklenebilir
        public string ImagePath { get; set; }    // Görsel yolu gösterim için eklendi
    }
}
