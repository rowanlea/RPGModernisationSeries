using FluentAssertions;
using RPGShop.Database;
using RPGShop.Model;
using System.Data;

namespace RPGShopTests.Database
{
    public class SqlServerResponseParserTests
    {
        SqlServerResponseParser _parser;

        public SqlServerResponseParserTests()
        {
            _parser = new();
        }

        [Test]
        public void GetItemListFromDataTable_WhenItemsExist_ReturnsItemList()
        {
            // TODO: Fill in when I get time. Holding off for now to allow series to progress.

            // Arrange
            DataTable dataTable = new DataTable();
            var expected = new List<Item>();

            // Act
            var result = _parser.GetItemListFromDataTable(dataTable);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetItemListFromDataTable_WhenNoItemsExist_ReturnsEmptyList()
        {
            // TODO: Fill in when I get time. Holding off for now to allow series to progress.
        }

        [Test]
        public void GetItemListFromDataTable_WhenBadFormat_ThrowsParsingException()
        {
            // TODO: Fill in when I get time. Holding off for now to allow series to progress.
        }

        [Test]
        public void GetItemFromDataTable_WhenItemExists_ReturnsItem()
        {
            // TODO: Fill in when I get time. Holding off for now to allow series to progress.
        }

        [Test]
        public void GetItemFromDataTable_WhenBadFormat_ThrowsParsingException()
        {
            // TODO: Fill in when I get time. Holding off for now to allow series to progress.
        }

        [Test]
        public void GetCountFromDataTable_WhenCalled_ReturnsCount()
        {
            // TODO: Fill in when I get time. Holding off for now to allow series to progress.
        }

        [Test]
        public void GetCountFromDataTable_WhenBadFormat_ThrowsParsingException()
        {
            // TODO: Fill in when I get time. Holding off for now to allow series to progress.
        }
    }
}
