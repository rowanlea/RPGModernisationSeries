using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace RPGShopTests.Controllers.Sales
{
    internal class GetSalesHistoryTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetSalesHistoryTests()
        {
            _factory = new ShopApiFactory();
        }

        [Test]
        public async Task WhenAskingForSalesHistory_ReturnsSalesHistory()
        {
            // Arrange
            var client = _factory.CreateClient();
            Sale[] foundSales = new Sale[0];

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Sales/GetSalesHistory");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                foundSales = JsonConvert.DeserializeObject<Sale[]>(jsonResponse);
            }

            // Assert
            foundSales.Count().Should().Be(1);
            foundSales[0].customerName.Should().Be("Rowan");
            foundSales[0].items.Count().Should().Be(2);
            foundSales[0].price.Should().Be(1.1f);
        }
    }
}
