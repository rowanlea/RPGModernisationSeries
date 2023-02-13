namespace RPGShopTests.Controllers.Sales
{
    public class CustomerOrder
    {
        public Item[] items { get; set; }
        public Customerdetails customerDetails { get; set; }
        public bool isTab { get; set; }
    }

    public class Customerdetails
    {
        public string name { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int price { get; set; }
        public int count { get; set; }
    }

}
