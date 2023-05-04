using MongoDB.Bson.Serialization.Attributes;

namespace RPGShop.Model
{
    [BsonIgnoreExtraElements]
    public class Sale
    {
        public string CustomerName { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public double Price { get; set; }
    }
}
