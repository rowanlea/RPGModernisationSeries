﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGShopTests.Controllers.Stock
{
    internal class GetItemsByTypeTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetItemsByTypeTests()
        {
            _factory = new ShopApiFactory();
        }

        [Test]
        public async Task WhenAskingForEquip_ReturnsSwordAsFirst()
        {
            // Arrange
            var client = _factory.CreateClient();
            Item[] foundItem = new Item[0];

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Stock/GetItemsByType?type=Equip");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                foundItem = JsonConvert.DeserializeObject<Item[]>(jsonResponse);
            }

            // Assert
            foundItem[0].ID.Should().Be(1);
            foundItem[0].Name.Should().Be("Steel Sword");
            foundItem[0].Description.Should().Be("A basic sword that deals damage to an enemy.");
            foundItem[0].Type.Should().Be("Equip");
            foundItem[0].Price.Should().Be(10.99f);
            foundItem[0].Count.Should().Be(0);
        }

        [Test]
        public async Task WhenAskingForBadType_ReturnsNotFoundError()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("https://localhost:7131/Shop/Stock/GetItemsByType?type=Bad%20Item%20Type");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
