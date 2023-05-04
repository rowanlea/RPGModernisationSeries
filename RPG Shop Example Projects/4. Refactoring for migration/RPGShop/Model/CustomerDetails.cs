using MongoDB.Bson.Serialization.Attributes;

namespace RPGShop.Model
{
    [BsonIgnoreExtraElements]
    public class CustomerDetails
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
