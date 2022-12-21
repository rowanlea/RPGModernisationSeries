namespace DatabaseSetup.SqlQueryStrings
{
    internal class DatabaseSetupQueries
    {
        internal static string CreateDatabase = "IF DB_ID (N'Inventory') IS NULL CREATE DATABASE Inventory";
        internal static string CreateItemsTable = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Items' and xtype='U') CREATE TABLE Items(ID INT NOT NULL,\"Name\" VARCHAR(200) NOT NULL,Price DECIMAL(7,2) NOT NULL,DescriptionID INT NOT NULL,TypeID INT NOT NULL);";
        internal static string CreateDescriptionsTable = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Descriptions' and xtype='U')CREATE TABLE Descriptions(ID INT NOT NULL,\"Description\" VARCHAR(500) NOT NULL);";
        internal static string CreateItemTypeTable = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ItemType' and xtype='U')CREATE TABLE ItemType(ID INT NOT NULL,\"Type\" VARCHAR(20) NOT NULL)";
        internal static string CreateStockTable = "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Stock' and xtype='U')CREATE TABLE Stock(ItemID INT NOT NULL,\"Count\" INT NOT NULL);";
        internal static string AllTables = $"{CreateItemsTable}{CreateDescriptionsTable}{CreateItemTypeTable}{CreateStockTable}";
    }
}
