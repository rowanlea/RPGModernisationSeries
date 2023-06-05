namespace RPGShop.Model
{
    public class CustomerOrder
    {
        public List<Item> Items { get; set; } = null!;
        public CustomerDetails CustomerDetails { get; set; } = null!;
        public bool IsTab { get; set; }
    }
}
