using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShoppingMongo.Entities
{
    public class Slider
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SliderId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Image { get; set; }
    }
}
