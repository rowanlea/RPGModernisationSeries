using RPGShop.Model;

namespace RPGShop.Database
{
    public interface ISqlDatabase
    {
        IEnumerable<Item> GetAllItems();
        Item GetItemByName(string itemName);
        IEnumerable<Item> GetItemsbyType(string typeName);
        int GetStockForItem(string itemName);
        void AddStock(string itemName, int numberOfStockToAdd);
        void RemoveStock(string itemName, int numberOfStockToRemove);
    }
}
