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

        }

        [Test]
        public void GetItemListFromDataTable_WhenBadFormat_ThrowsParsingException()
        {

        }


        [Test]
        public void GetItemFromDataTable_WhenItemExists_ReturnsItem()
        {

        }

        [Test]
        public void GetItemFromDataTable_WhenBadFormat_ThrowsParsingException()
        {

        }


        [Test]
        public void GetCountFromDataTable_WhenCalled_ReturnsCount()
        {

        }

        [Test]
        public void GetCountFromDataTable_WhenBadFormat_ThrowsParsingException()
        {

        }
    }
}
