using MongoDB.Bson.Serialization.Attributes;

namespace RPGShop.Model
{
    [BsonIgnoreExtraElements]
    public class CustomerDetails
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
