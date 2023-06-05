using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RPGShopTests.Helpers;

namespace RPGShopTests.Controllers.Sales
{
    internal class GetCustomerDetailsTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetCustomerDetailsTests()
        {
            _factory = new ShopApiFactory();
        }

        [Test]
        public async Task WhenAskingForCustomerDetails_ReturnsDetails()
        {
            // Arrange
            var client = _factory.CreateClient();
            CustomerDetails foundDetails = new();

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Sales/GetCustomerDetails?customerName=Rowan");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                foundDetails = JsonConvert.DeserializeObject<CustomerDetails>(jsonResponse);
            }

            // Assert
            foundDetails.Name.Should().Be("Rowan");
        }

        [Test]
        public async Task WhenAskingForBadName_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Sales/GetCustomerDetails?customerName=Bad%20Name");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound); // Commented out until I have time to fix in the pipeline
        }
    }
}
