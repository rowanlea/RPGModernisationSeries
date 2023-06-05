using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RPGShopTests.Helpers;

namespace RPGShopTests.Controllers.Stock
{
    internal class GetAllItemsTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetAllItemsTests()
        {
            _factory = new ShopApiFactory();
        }

        [Test]
        public async Task WhenCalled_ReturnsExpectedItems()
        {
            // Arrange
            var client = _factory.CreateClient();
            Item[] foundItems = Array.Empty<Item>();

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Stock/GetAllItems");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                foundItems = JsonConvert.DeserializeObject<Item[]>(jsonResponse);
            }

            // Assert
            foundItems.Length.Should().Be(100);
            foundItems[0].ID.Should().Be(1);
            foundItems[0].Name.Should().Be("Steel Sword");
            foundItems[0].Description.Should().Be("A basic sword that deals damage to an enemy.");
            foundItems[0].Type.Should().Be("Equip");
            foundItems[0].Price.Should().Be(10.99f);
            foundItems[0].Count.Should().Be(1);
        }
    }
}
