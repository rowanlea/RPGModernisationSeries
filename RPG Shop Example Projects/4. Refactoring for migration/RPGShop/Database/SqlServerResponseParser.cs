using RPGShop.Model;
using System.Data;

namespace RPGShop.Database
{
    public class SqlServerResponseParser
    {
        public List<Item> GetItemListFromDataTable(DataTable dataTable)
        {
            return (from DataRow row in dataTable.Rows
                    select new Item()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        Type = row["Type"].ToString(),
                        Price = Convert.ToDouble(row["Price"].ToString())
                    }).ToList();
        }

        public Item GetItemFromDataTable(DataTable dataTable)
        {
            DataRow row = dataTable.Rows[0];
            Item item = new Item()
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Type = row["Type"].ToString(),
                Price = Convert.ToDouble(row["Price"].ToString())
            };
            return item;
        }

        internal int GetCountFromDataTable(DataTable dataTable)
        {
            return (int)dataTable.Rows[0][0];
        }
    }
}
