using RPGShop.Model;
using System.Data;
using System.Data.SqlClient;

namespace RPGShop.Database
{
    public class SQLDatabase : ISqlDatabase
    {
        private readonly SqlConnection _dbConnection;
        private readonly SqlServerResponseParser _parser;

        public SQLDatabase(IConfiguration config)
        {
            string? dbConnectionString = config.GetConnectionString("SqlConnectionString");
            _dbConnection = new SqlConnection(dbConnectionString);
            _parser = new();
        }

        public IEnumerable<Item> GetAllItems()
        {
            string query = @"SELECT Items.ID, Items.Name, Items.Price, Descriptions.Description, ItemType.Type
            FROM Items
            INNER JOIN Descriptions ON Items.DescriptionID = Descriptions.ID
            INNER JOIN ItemType ON Items.TypeID = ItemType.ID;";

            DataTable dataTable = QueryDatabase(query);
            List<Item> itemList = _parser.GetItemListFromDataTable(dataTable);
            return itemList;
        }

        public Item GetItemByName(string itemName)
        {
            string query = @"SELECT Items.ID, Items.Name, Items.Price, Descriptions.Description, ItemType.Type
            FROM Items
            INNER JOIN Descriptions ON Items.DescriptionID = Descriptions.ID
            INNER JOIN ItemType ON Items.TypeID = ItemType.ID
            WHERE Items.Name = @Name;";

            List<SqlParameter> parameters = new() { new SqlParameter("@Name", itemName) };
            DataTable dataTable = QueryDatabase(query, parameters);
            Item item = _parser.GetItemFromDataTable(dataTable);
            return item;
        }

        public IEnumerable<Item> GetItemsbyType(string typeName)
        {
            string query = @"SELECT Items.ID, Items.Name, Items.Price, Descriptions.Description, ItemType.Type
            FROM Items
            INNER JOIN Descriptions ON Items.DescriptionID = Descriptions.ID
            INNER JOIN ItemType ON Items.TypeID = ItemType.ID
            WHERE ItemType.Type = @Type;";

            List<SqlParameter> parameters = new() { new SqlParameter("@Type", typeName) };
            DataTable dataTable = QueryDatabase(query, parameters);
            List<Item> itemList = _parser.GetItemListFromDataTable(dataTable);
            return itemList;
        }

        public int GetStockForItem(string itemName)
        {
            string query = @"SELECT Count FROM Stock
            INNER JOIN Items ON Stock.ItemID = Items.ID
            WHERE Items.Name = @Name;";

            List<SqlParameter> parameters = new() { new SqlParameter("@Name", itemName) };
            DataTable dataTable = QueryDatabase(query, parameters);
            int stockCount = SqlServerResponseParser.GetCountFromDataTable(dataTable);
            return stockCount;
        }

        public void AddStock(string itemName, int numberOfStockToAdd)
        {
            UpdateStock(itemName, numberOfStockToAdd);
        }

        public void RemoveStock(string itemName, int numberOfStockToRemove)
        {
            UpdateStock(itemName, -numberOfStockToRemove);
        }

        private void UpdateStock(string itemName, int numberToUpdate)
        {
            string query = @$"
            UPDATE Stock
            SET Stock.Count = Stock.Count + '{numberToUpdate}'
            FROM Stock
            INNER JOIN Items ON Stock.ItemID = Items.ID
            WHERE Items.Name = '{itemName}';";

            using SqlCommand command = new(query, _dbConnection);
            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();
        }

        private DataTable QueryDatabase(string query, List<SqlParameter>? parameters = default)
        {
            ArgumentNullException.ThrowIfNull(query);
            ArgumentNullException.ThrowIfNull(parameters);

            DataTable dataTable = new ();
            using (SqlCommand command = new(query, _dbConnection))
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }

                using SqlDataAdapter reader = new(command);
                reader.Fill(dataTable);
            }

            return dataTable;
        }
    }
}
