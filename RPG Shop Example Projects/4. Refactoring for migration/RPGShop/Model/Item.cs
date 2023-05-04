using MongoDB.Bson.Serialization.Attributes;

namespace RPGShop.Model
{
    [BsonIgnoreExtraElements]
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
