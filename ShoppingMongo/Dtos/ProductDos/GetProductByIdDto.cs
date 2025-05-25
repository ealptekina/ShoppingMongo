namespace ShoppingMongo.Dtos.ProductDos
{
    public class GetProductByIdDto
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
        public List<string> ImagePaths { get; set; } = new List<string>();


    }
}
