using MongoDB.Driver;
using RPGShop.Model;

namespace RPGShop.Database
{
    public class NoSqlDatabase : INoSqlDatabase
    {
        private readonly MongoClient _dbClient;
        private readonly IMongoDatabase _database;

        public NoSqlDatabase(IConfiguration config)
        {
            string? dbConnectionString = config.GetConnectionString("NoSqlConnectionString");
            _dbClient = new MongoClient(dbConnectionString);
            _database = _dbClient.GetDatabase("Sales");
        }

        public void MakeSale(Sale sale)
        {
            var collection = _database.GetCollection<Sale>("SalesHistory");
            collection.InsertOne(sale);
        }

        public IEnumerable<Sale> GetSalesHistory()
        {
            var collection = _database.GetCollection<Sale>("SalesHistory");
            var salesHistory = collection.Find(_ => true);
            return salesHistory.ToList();
        }

        public void AddToTab(Tab tab)
        {
            var collection = _database.GetCollection<Tab>("Tab");
            collection.InsertOne(tab);
        }

        public Tab GetTabForCustomer(string customerName)
        {
            var collection = _database.GetCollection<Tab>("Tab");
            var filter = Builders<Tab>.Filter.Eq("CustomerName", customerName);
            var customerTab = collection.Find(filter).FirstOrDefault();
            return customerTab;
        }

        public void RemoveFromTab(string customerName)
        {
            var collection = _database.GetCollection<CustomerDetails>("Tab");
            var filter = Builders<CustomerDetails>.Filter.Eq("CustomerName", customerName);
            collection.DeleteOne(filter);
        }

        public void AddCustomerDetails(CustomerDetails customerDetails)
        {
            var collection = _database.GetCollection<CustomerDetails>("CustomerDetails");
            collection.InsertOne(customerDetails);
        }

        public CustomerDetails GetCustomerByName(string customerName)
        {
            var collection = _database.GetCollection<CustomerDetails>("CustomerDetails");
            var filter = Builders<CustomerDetails>.Filter.Eq("Name", customerName);
            var customer = collection.Find(filter).FirstOrDefault();
            return customer;
        }
    }
}
