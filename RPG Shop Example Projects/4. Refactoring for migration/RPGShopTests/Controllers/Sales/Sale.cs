namespace RPGShopTests.Controllers.Sales
{
    public class Sale
    {
        public string customerName { get; set; }
        public RPGShop.Model.Item[] items { get; set; }
        public float price { get; set; }
    }
}
