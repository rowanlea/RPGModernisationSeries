using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RPGShopTests.Controllers.Stock
{
    internal class RestockTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RestockTests()
        {
            _factory = new ShopApiFactory();
        }

        private class Outbound
        {
            public string itemName { get; set; }
            public int numberToRestock { get; set; }
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
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
