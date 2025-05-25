namespace ShoppingMongo.Dtos.ProductDos
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Status { get; set; }
        public int StockCount { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public List<IFormFile> ImageFiles { get; set; } // 📥 Dosya yükleme için
        public List<string> ImageUrls { get; set; } = new List<string>();
        // 💾 Yüklenenlerin URL'lerini saklamak için

    }
}
