using FluentAssertions;
using RPGShop.Database;

namespace RPGShop.IntegrationTests
{
    internal class SQLDatabaseTests
    {
        [Test]
        public void AddStock_WhenStockAdded_NewStockCountIncreased()
        {
            // Arrange
            SQLDatabase database = new();
            string itemName = "Steel Sword";
            int numberToAdd = 5;

            // Act
            int currentCount = database.GetStockForItem(itemName);
            database.AddStock(itemName, numberToAdd);
            int finalCount = database.GetStockForItem(itemName);

            // Assert
            finalCount.Should().Be(currentCount + numberToAdd);
        }

        [Test]
        public void RemoveStock_WhenStockRemoved_NewStockCountDecreased()
        {
            // Arrange
            SQLDatabase database = new();
            string itemName = "Steel Sword";
            int numberToRemove = 2;
            
            // Act
            int currentCount = database.GetStockForItem(itemName);
            database.RemoveStock(itemName, numberToRemove);
            int finalCount = database.GetStockForItem(itemName);

            // Assert
            finalCount.Should().Be(currentCount - numberToRemove);
        }
    }
}
