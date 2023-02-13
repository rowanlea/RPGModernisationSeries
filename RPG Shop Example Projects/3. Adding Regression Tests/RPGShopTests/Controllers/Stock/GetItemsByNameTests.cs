using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace RPGShopTests.Controllers.Stock
{
    internal class GetItemsByNameTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetItemsByNameTests()
        {
            _factory = new ShopApiFactory();
        }

        [Test]
        public async Task WhenAskingForSword_ReturnsSword()
        {
            // Arrange
            var client = _factory.CreateClient();
            Item foundItem = new();

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Stock/GetItemsByName?name=Steel%20Sword");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                foundItem = JsonConvert.DeserializeObject<Item>(jsonResponse);
            }

            // Assert
            foundItem.ID.Should().Be(1);
            foundItem.Name.Should().Be("Steel Sword");
            foundItem.Description.Should().Be("A basic sword that deals damage to an enemy.");
            foundItem.Type.Should().Be("Equip");
            foundItem.Price.Should().Be(10.99f);
            foundItem.Count.Should().Be(0);
        }

        [Test]
        public async Task WhenAskingForBadItem_ReturnsNotFoundError()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Stock/GetItemsByName?name=Bad%20Item%20Name");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
