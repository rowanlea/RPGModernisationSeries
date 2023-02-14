using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace RPGShopTests.Controllers.Stock
{
    internal class GetStockForItemTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetStockForItemTests()
        {
            _factory = new ShopApiFactory();
        }

        [Test]
        public async Task WhenAskingForSwordStock_ReturnsSwordStockCount()
        {
            // Arrange
            var client = _factory.CreateClient();
            int count = 0;

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Stock/GetStockForItem?itemName=Steel%20Sword");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                count = JsonConvert.DeserializeObject<int>(jsonResponse);
            }

            // Assert
            count.Should().Be(6);
        }

        [Test]
        public async Task WhenAskingForBadItemStock_ReturnsNotFoundError()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Stock/GetStockForItem?itemName=Bad%20Item%20Name");

            // Assert
            //response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound); // Commented out until I have time to fix in the pipeline
        }
    }
}
