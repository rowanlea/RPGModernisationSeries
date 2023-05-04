namespace RPGShopTests.Controllers.Sales
{
    public class Sale
    {
        public string? CustomerName { get; set; }
        public RPGShop.Model.Item[] Items { get; set; } = Array.Empty<RPGShop.Model.Item>();
        public float Price { get; set; }
    }
}
