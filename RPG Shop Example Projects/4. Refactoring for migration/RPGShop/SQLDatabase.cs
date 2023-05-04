﻿using System.Data;
using System.Data.SqlClient;

namespace RPGShop
{
    public class SQLDatabase : ISqlDatabase
    {
        private string _dbConnectionString = @"Server=localhost\SQLEXPRESS;Database=Inventory;Trusted_Connection=True;";
        private SqlConnection _dbConnection;

        public SQLDatabase()
        {
            _dbConnection = new SqlConnection(_dbConnectionString);
        }

        public List<Item> GetAllItems()
        {
            string query = @"SELECT Items.ID, Items.Name, Items.Price, Descriptions.Description, ItemType.Type
            FROM Items
            INNER JOIN Descriptions ON Items.DescriptionID = Descriptions.ID
            INNER JOIN ItemType ON Items.TypeID = ItemType.ID;";

            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, _dbConnection))
            {
                using (SqlDataAdapter reader = new SqlDataAdapter(command))
                {
                    reader.Fill(dataTable);
                }
            }
            List<Item> itemList = new List<Item>();
            itemList = (from DataRow row in dataTable.Rows
                        select new Item()
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Name = row["Name"].ToString(),
                            Description = row["Description"].ToString(),
                            Type = row["Type"].ToString(),
                            Price = Convert.ToDouble(row["Price"].ToString())
                        }).ToList();

            return itemList;
        }

        public Item GetItemByName(string itemName)
        {
            string query = @"SELECT Items.ID, Items.Name, Items.Price, Descriptions.Description, ItemType.Type
            FROM Items
            INNER JOIN Descriptions ON Items.DescriptionID = Descriptions.ID
            INNER JOIN ItemType ON Items.TypeID = ItemType.ID
            WHERE Items.Name = @Name;";

            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, _dbConnection))
            {
                command.Parameters.AddWithValue("@Name", itemName);

                using (SqlDataAdapter reader = new SqlDataAdapter(command))
                {
                    reader.Fill(dataTable);
                }
            }
            var row = dataTable.Rows[0];
            var item = new Item()
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Type = row["Type"].ToString(),
                Price = Convert.ToDouble(row["Price"].ToString())
            };
            return item;
        }

        public List<Item> GetItemsbyType(string typeName)
        {
            string query = @"SELECT Items.ID, Items.Name, Items.Price, Descriptions.Description, ItemType.Type
            FROM Items
            INNER JOIN Descriptions ON Items.DescriptionID = Descriptions.ID
            INNER JOIN ItemType ON Items.TypeID = ItemType.ID
            WHERE ItemType.Type = @Type;";

            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, _dbConnection))
            {
                command.Parameters.AddWithValue("@Type", typeName);

                using (SqlDataAdapter reader = new SqlDataAdapter(command))
                {
                    reader.Fill(dataTable);
                }
            }

            List<Item> itemList = new List<Item>();
            itemList = (from DataRow row in dataTable.Rows
                        select new Item()
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Name = row["Name"].ToString(),
                            Description = row["Description"].ToString(),
                            Type = row["Type"].ToString(),
                            Price = Convert.ToDouble(row["Price"].ToString())
                        }).ToList();

            return itemList;
        }

        public int GetStockForItem(string itemName)
        {
            string query = @"SELECT Count FROM Stock
            INNER JOIN Items ON Stock.ItemID = Items.ID
            WHERE Items.Name = @Name;";

            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, _dbConnection))
            {
                command.Parameters.AddWithValue("@Name", itemName);


                using (SqlDataAdapter reader = new SqlDataAdapter(command))
                {
                    reader.Fill(dataTable);
                }
            }
            return (int)dataTable.Rows[0][0];
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

            using (SqlCommand command = new SqlCommand(query, _dbConnection))
            {
                _dbConnection.Open();
                command.ExecuteNonQuery();
                _dbConnection.Close();
            }
        }
    }
}
