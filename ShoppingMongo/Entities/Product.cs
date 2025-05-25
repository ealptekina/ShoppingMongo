using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShoppingMongo.Entities
{

    /// <summary>
    /// Ürün varlık (entity) sınıfı - MongoDB koleksiyonunda ürün bilgilerini temsil eder.
    /// </summary>
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        [BsonElement("ImageUrls")] // MongoDB'de alan adı böyleyse bu şekilde belirt
        public List<string> ImageUrls { get; set; } = new();
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int StockCount { get; set; }
        public string CategoryId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
    }
}
