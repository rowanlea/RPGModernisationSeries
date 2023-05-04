using FluentAssertions;
using NSubstitute;
using RPGShop.Model;
using System.Net.Http.Json;

namespace RPGShopTests.Controllers.Sales
{
    internal class SellItemsToCustomerTests
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
            customerOrder.IsTab = isTab;

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7131/Shop/Sales/SellItemsToCustomer", customerOrder);

            // Assert
            if(isTab)
                noSqlDatabase.Received().AddToTab(Arg.Is<Tab>(x => x.Items.First().Name == "Steel Sword"));
            else
                noSqlDatabase.Received().MakeSale(Arg.Is<RPGShop.Model.Sale>(x => x.Items.First().Name == "Steel Sword"));
        }

        [Test]
        public async Task WhenSubmittingMissingCustomerDetails_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            CustomerOrder customerOrder = GetFakeCustomerOrder();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            customerOrder.CustomerDetails.Address = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7131/Shop/Sales/SellItemsToCustomer", customerOrder);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task WhenSubmittingBadItemsName_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            CustomerOrder customerOrder = GetFakeCustomerOrder();
            customerOrder.Items[0].Name = "Bad item name";

            // Act
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7131/Shop/Sales/SellItemsToCustomer", customerOrder);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound); // Commented out until I have time to fix in the pipeline
        }

        private static CustomerOrder GetFakeCustomerOrder()
        {
            CustomerOrder customerOrder = new()
            {
                // Set up customer details
                CustomerDetails = new Customerdetails { Address = "fake", Name = "fake", PhoneNumber = "fake" },

                // Set up items being sold
                Items = new Item[1]
            };

            customerOrder.Items[0] = new Item { Name = "Steel Sword", Count = 1, Description = "fake", Type = "Equip" };

            // Is the order to be added to the customer's tab?
            customerOrder.IsTab = false;

            return customerOrder;
        }
    }
}
