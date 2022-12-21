using DatabaseSetup;

Console.WriteLine("Setting up database...");

Database database = new();
database.CreateDatabase();
Console.WriteLine("Database created");

database.CreateTables();
Console.WriteLine("Tables created");

database.CreateTableData();
Console.WriteLine("Mock table data added");

Console.WriteLine("Database setup completed!");