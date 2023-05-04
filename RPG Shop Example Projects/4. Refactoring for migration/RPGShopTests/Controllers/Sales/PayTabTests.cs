using FluentAssertions;
using NSubstitute;
using System.Text;

namespace RPGShopTests.Controllers.Sales
{
    internal class PayTabTests
    {
        private readonly ShopApiFactory _factory;

        public PayTabTests()
        {
            _factory = new ShopApiFactory();
        }

        [Test]
        public async Task WhenGivenValidCustomerName_ReturnsOk()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:7131/Shop/Sales/PayTab?customerName=Rowan", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task WhenCalled_RemovesCustomerFromTab()
        {
            // Arrange
            var client = _factory.CreateClient();
            var noSqlDatabase = _factory.GetMockedNoSql();

            var content = new StringContent("", Encoding.UTF8, "application/json");

            // Act
            await client.PostAsync("https://localhost:7131/Shop/Sales/PayTab?customerName=Rowan", content);

            // Assert
            noSqlDatabase.Received().RemoveFromTab("Rowan");
        }

        [Test]
        public async Task WhenCustomerTabContainsTwoSwords_MakesSaleWithCorrectPrice()
        {
            // Arrange
            var client = _factory.CreateClient();
            var noSqlDatabase = _factory.GetMockedNoSql();

            var content = new StringContent("", Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await client.PostAsync("https://localhost:7131/Shop/Sales/PayTab?customerName=Rowan", content);

            // Assert
            noSqlDatabase.Received().MakeSale(Arg.Is<RPGShop.Model.Sale>(x => x.Price == 21.98f));
        }

        [Test]
        public async Task WhenCustomerNameBad_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            var content = new StringContent("", Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await client.PostAsync("https://localhost:7131/Shop/Sales/PayTab?customerName=Bob", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound); // Commented out until I have time to fix in the pipeline
        }
    }
}
