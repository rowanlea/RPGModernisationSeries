using System.Data.SqlClient;
using System.Data;

namespace RPGShop
{
    public interface ISqlDatabase
    {
        List<Item> GetAllItems();
        Item GetItemByName(string itemName);
        List<Item> GetItemsbyType(string typeName);
        int GetStockForItem(string itemName);
        void AddStock(string itemName, int numberOfStockToAdd);
        void RemoveStock(string itemName, int numberOfStockToRemove);
    }
}
