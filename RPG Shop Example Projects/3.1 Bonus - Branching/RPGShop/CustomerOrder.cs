namespace RPGShop
{
    public class CustomerOrder
    {
        public List<Item> Items { get; set; }
        public CustomerDetails CustomerDetails { get; set; }
        public bool IsTab { get; set; }
    }
}
