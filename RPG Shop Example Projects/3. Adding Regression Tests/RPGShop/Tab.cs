using MongoDB.Bson.Serialization.Attributes;

namespace RPGShop
{
    [BsonIgnoreExtraElements]
    public class Tab
    {
        public string CustomerName { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
