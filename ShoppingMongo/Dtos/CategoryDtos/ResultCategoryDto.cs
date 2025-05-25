namespace ShoppingMongo.Dtos.CategoryDtos
{
    public class ResultCategoryDto
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }    // Listeleme için görsel yolu eklendi
        public string Description { get; set; }   
    }
}
