using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingMongo.Entities
{
    /// <summary>
    /// Kategori varlık (entity) sınıfı - MongoDB koleksiyonu için kullanılır.
    /// </summary>
    public class Category
    {

        /// <summary>
        /// MongoDB tarafından otomatik olarak oluşturulan kategori benzersiz kimliği.
        /// [BsonId] özelliği bu alanın birincil anahtar olduğunu belirtir.
        /// [BsonRepresentation(BsonType.ObjectId)] bu alanın string olarak tutulmasına rağmen
        /// MongoDB'de ObjectId olarak saklanmasını sağlar.
        /// </summary>

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
