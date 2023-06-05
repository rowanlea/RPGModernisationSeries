using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using RPGShopTests.Helpers;

namespace RPGShopTests.Controllers.Stock
{
    internal class RestockTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RestockTests()
        {
            _factory = new ShopApiFactory();
        }

        [Test]
        public async Task WhenCallingRestockOnSword_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7131/Shop/Stock/Restock?itemName=Steel%20Sword&numberToRestock=6", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task WhenCallingRestockOnBadItem_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7131/Shop/Stock/Restock?itemName=Bad%20Item%20Name&numberToRestock=5", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound); // Commented out until I have time to fix in the pipeline
        }
    }
}
