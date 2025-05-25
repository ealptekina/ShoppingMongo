using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingMongo.Dtos.ProductDos
{
    public class UpdateProductDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Status { get; set; }
        public int StockCount { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public List<IFormFile> NewImageFiles { get; set; } // Yeni yüklenecek görseller
        public List<string> ImagePaths { get; set; } = new List<string>(); // ✅ Mevcut görsellerin yollarını göstermek için:

        // İşte dropdown için Categories burada:
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

    }
}
