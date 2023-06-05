using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RPGShopTests.Helpers;

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
            Sale[] foundSales = Array.Empty<Sale>();

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Sales/GetSalesHistory");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                foundSales = JsonConvert.DeserializeObject<Sale[]>(jsonResponse);
            }

            // Assert
            foundSales.Length.Should().Be(1);
            foundSales[0].CustomerName.Should().Be("Rowan");
            foundSales[0].Items.Length.Should().Be(2);
            foundSales[0].Price.Should().Be(1.1f);
        }
    }
}
