using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RPGShop;

namespace RPGShopTests
{
    internal class ShopApiFactory : WebApplicationFactory<Program>
    {
        ISqlDatabase mockedSqlDatabase;
        INoSqlDatabase mockedNoSqlDatabase;

        public ShopApiFactory()
        {
            mockedSqlDatabase = Substitute.For<ISqlDatabase>();
            mockedNoSqlDatabase = Substitute.For<INoSqlDatabase>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Let the builder do its normal setup
            base.ConfigureWebHost(builder);

            // Take our builder and edit the service it has set up
            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();

                mockedSqlDatabase.GetItemByName("Steel Sword").Returns(GetSteelSwordItem());
                mockedSqlDatabase.GetItemByName("Bad Item Name").Throws<IndexOutOfRangeException>();

                // Link our mocked databases to their interface types
                var sqlDescriptor =
                    new ServiceDescriptor(
                        typeof(ISqlDatabase),
                        mockedSqlDatabase);

                var noSqlDescriptor =
                    new ServiceDescriptor(
                        typeof(INoSqlDatabase),
                        mockedNoSqlDatabase);

                // Switch out the services we want to mock
                services.Replace(sqlDescriptor);
                services.Replace(noSqlDescriptor);
            });
        }

        public INoSqlDatabase GetMockedNoSql()
        {
            return mockedNoSqlDatabase;
        }

        private static RPGShop.Item GetSteelSwordItem()
        {
            return new RPGShop.Item
            {
                Id = 1,
                Name = "Steel Sword",
                Description = "A basic sword that deals damage to an enemy.",
                Type = "Equip",
                Price = 10.99f,
                Count = 0
            };
        }
    }
}