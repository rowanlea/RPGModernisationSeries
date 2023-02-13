using FluentAssertions;
using NSubstitute;
using System.Net.Http.Json;

namespace RPGShopTests
{
    public class SellItemsToCustomerTests
    {
        private readonly ShopApiFactory _factory;

        public SellItemsToCustomerTests()
        {
            _factory = new ShopApiFactory();
        }

        [Test]
        public async Task WhenSubmittingSale_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            CustomerOrder customerOrder = GetFakeCustomerOrder();

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7131/Shop/Sales/SellItemsToCustomer", customerOrder);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task WhenSaleNotTab_NoSqlMakeSaleCalled(bool isTab)
        {
            // Arrange
            var client = _factory.CreateClient();
            var noSqlDatabase = _factory.GetMockedNoSql();
            CustomerOrder customerOrder = GetFakeCustomerOrder();
            customerOrder.isTab = isTab;

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7131/Shop/Sales/SellItemsToCustomer", customerOrder);

            // Assert
            if (isTab)
                noSqlDatabase.Received().AddToTab(Arg.Is<RPGShop.Tab>(x => x.Items.First().Name == "Steel Sword"));
            else
                noSqlDatabase.Received().MakeSale(Arg.Is<RPGShop.Sale>(x => x.Items.First().Name == "Steel Sword"));
        }

        [Test]
        public async Task WhenSubmittingBadItemsName_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            CustomerOrder customerOrder = GetFakeCustomerOrder();
            customerOrder.items[0].name = "Bad item name";

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7131/Shop/Sales/SellItemsToCustomer", customerOrder);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        private CustomerOrder GetFakeCustomerOrder()
        {
            CustomerOrder customerOrder = new();

            // Set up customer details
            customerOrder.customerDetails = new Customerdetails { address = "fake", name = "fake", phoneNumber = "fake" };

            // Set up items being sold
            customerOrder.items = new Item[1];
            customerOrder.items[0] = new Item { name = "Steel Sword", count = 1, description = "fake", type = "Equip" };

            // Is the order to be added to the customer's tab?
            customerOrder.isTab = false;

            return customerOrder;
        }
    }
}