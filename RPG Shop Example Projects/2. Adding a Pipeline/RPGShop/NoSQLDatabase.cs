using MongoDB.Driver;

namespace RPGShop
{
    internal class NoSQLDatabase
    {
        MongoClient _dbClient;
        IMongoDatabase _database;

        internal NoSQLDatabase()
        {
            _dbClient = new MongoClient("mongodb://localhost:27017");
            _database = _dbClient.GetDatabase("Sales");fail
        }

        internal void MakeSale(Sale sale)
        {
            var collection = _database.GetCollection<Sale>("SalesHistory");
            collection.InsertOne(sale);
        }

        internal List<Sale> GetSalesHistory()
        {
            var collection = _database.GetCollection<Sale>("SalesHistory");
            var salesHistory = collection.Find(_ => true);
            return salesHistory.ToList();
        }

        internal void AddToTab(Tab tab)
        {
            var collection = _database.GetCollection<Tab>("Tab");
            collection.InsertOne(tab);
        }

        internal Tab GetTabForCustomer(string customerName)
        {
            var collection = _database.GetCollection<Tab>("Tab");
            var filter = Builders<Tab>.Filter.Eq("CustomerName", customerName);
            var customerTab = collection.Find(filter).FirstOrDefault();
            return customerTab;
        }

        internal void RemoveFromTab(string customerName)
        {
            var collection = _database.GetCollection<CustomerDetails>("Tab");
            var filter = Builders<CustomerDetails>.Filter.Eq("CustomerName", customerName);
            collection.DeleteOne(filter);
        }

        internal void AddCustomerDetails(CustomerDetails customerDetails)
        {
            var collection = _database.GetCollection<CustomerDetails>("CustomerDetails");
            collection.InsertOne(customerDetails);
        }

        internal CustomerDetails GetCustomerByName(string customerName)
        {
            var collection = _database.GetCollection<CustomerDetails>("CustomerDetails");
            var filter = Builders<CustomerDetails>.Filter.Eq("Name", customerName);
            var customer = collection.Find(filter).FirstOrDefault();
            return customer;
        }
    }
}
