namespace RPGShopTests.Controllers.Sales
{
    public class CustomerOrder
    {
        public Item[] Items { get; set; } = null!;
        public Customerdetails CustomerDetails { get; set; } = null!;
        public bool IsTab { get; set; } = false;
    }

    public class Customerdetails
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }

    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Type { get; set; } = null!;
        public int Price { get; set; }
        public int Count { get; set; }
    }

}
