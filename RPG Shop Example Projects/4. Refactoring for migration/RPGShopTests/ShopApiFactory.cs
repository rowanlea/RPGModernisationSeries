using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RPGShop.Database;
using RPGShop.Model;

namespace RPGShopTests
{
    internal class ShopApiFactory : WebApplicationFactory<Program>
    {
        ISqlDatabase mockedSqlDatabase;
        INoSqlDatabase mockedNoSqlDatabase;

        public ShopApiFactory()
        {
            mockedSqlDatabase = Substitute.For<ISqlDatabase>();
            mockedNoSqlDatabase = Substitute.For<INoSqlDatabase>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Let the builder do its normal setup
            base.ConfigureWebHost(builder);

            // Take our builder and edit the service it has set up
            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();

                // Setup mocked databases
                mockedSqlDatabase.GetAllItems().Returns(GetFakeItems());
                mockedSqlDatabase.GetItemByName("Steel Sword").Returns(GetSteelSwordItem());
                mockedSqlDatabase.GetItemByName("Bad Item Name").Throws<IndexOutOfRangeException>();
                mockedSqlDatabase.GetItemsbyType("Equip").Returns(GetFakeItems());
                mockedSqlDatabase.GetItemsbyType("Bad Item Type").Throws<IndexOutOfRangeException>();
                mockedSqlDatabase.GetStockForItem("Steel Sword").Returns(6);
                mockedSqlDatabase.GetStockForItem("Bad Item Name").Throws<IndexOutOfRangeException>();
                mockedSqlDatabase.When(x => x.AddStock("Bad Item Name", 5)).Throw<IndexOutOfRangeException>();

                mockedNoSqlDatabase.GetCustomerByName("Rowan").Returns(GetFakeDetails());
                mockedNoSqlDatabase.GetCustomerByName("Bad Name").Throws<IndexOutOfRangeException>();
                mockedNoSqlDatabase.GetSalesHistory().Returns(GetFakeHistory());
                mockedNoSqlDatabase.GetTabForCustomer("Rowan").Returns(GetFakeTab());

                // Link our mocked databases to their interface types
                var sqlDescriptor =
                    new ServiceDescriptor(
                        typeof(ISqlDatabase),
                        mockedSqlDatabase);

                var noSqlDescriptor =
                    new ServiceDescriptor(
                        typeof(INoSqlDatabase),
                        mockedNoSqlDatabase);

                // Switch out the services we want to mock
                services.Replace(sqlDescriptor);
                services.Replace(noSqlDescriptor);
            });
        }

        public INoSqlDatabase GetMockedNoSql()
        {
            return mockedNoSqlDatabase;
        }

        private List<RPGShop.Model.Item> GetFakeItems()
        {
            // Set up item array with 100 empty values
            RPGShop.Model.Item[] itemList = new RPGShop.Model.Item[100];

            // Set first value to object we are testing against
            itemList[0] = GetSteelSwordItem();

            return itemList.ToList();
        }

        private static RPGShop.Model.Item GetSteelSwordItem()
        {
            return new RPGShop.Model.Item
            {
                Id = 1,
                Name = "Steel Sword",
                Description = "A basic sword that deals damage to an enemy.",
                Type = "Equip",
                Price = 10.99f,
                Count = 1
            };
        }

        private static CustomerDetails GetFakeDetails()
        {
            return new RPGShop.Model.CustomerDetails
            {
                Name = "Rowan",
                Address = "Address",
                PhoneNumber = "123456789"
            };
        }

        private List<Sale> GetFakeHistory()
        {
            List<Sale> sales = new();

            var items = new List<RPGShop.Model.Item> {
                GetSteelSwordItem(),
                GetSteelSwordItem()
            };

            sales.Add(new Sale { CustomerName = "Rowan", Items = items, Price = 1.1f });

            return sales;
        }

        private Tab GetFakeTab()
        {
            Tab tab = new()
            {
                CustomerName = "Rowan",
                Items = new List<RPGShop.Model.Item> { GetSteelSwordItem(), GetSteelSwordItem() }
            };

            return tab;
        }

    }
}
