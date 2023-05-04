namespace RPGShop.Database
{
    public class SqlParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public SqlParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
