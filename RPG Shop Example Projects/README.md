# RPG Shop Example Projects

## Project Overview
This project aims to improve the inventory management and transaction processes for employees at a Role Playing Game shop. It provides easy-to-use endpoints for general employees to make sales and access item information, as well as specialized endpoints for the shop manager to track sales data and restock items as needed. By using this project, shop employees can efficiently handle the day-to-day operations of the store, enabling them to provide a better shopping experience for their customers.

As part of my YouTube channel: [Rowan Lea](https://www.youtube.com/@rowan-lea), I will be continually developing and improving this project. It's important to note that the current design of this project is meant to be incomplete, and may appear poorly written. This is intentional for the video series progression, and will enable me to illustrate the content and improvements being made. To stay updated on the progress of this project, be sure to check out the channel or the [repo](https://github.com/rowanlea/RPGModernisationSeries) itself.

## Project Setup
### Note
This project and setup are to demonstrate how all of the concepts in the video series can be used. If you are just taking the concepts to apply to your own project, feel free to skip some or all of these steps.

### Pre-requisites
Before proceeding with this installation, it is assumed that you have a basic understanding of C# and .NET. If you need a refresher on these concepts, there are many resources available online to help you get up to speed.

### .NET version
For this project, I am utilizing .NET 7, which is the most current version at the time of writing. While the project was originally written in .NET 6, there were no issues with upgrading to .NET 7. However, if you prefer to use .NET 6, you can easily make that change by downgrading the version in the project settings.

### SQL Server Setup
Go here to install [SQL Server](https://www.microsoft.com/en-gb/sql-server/sql-server-downloads), choose SQL Express. Select "basic" and accept the defaults.

If you want a convenient way to view your data, consider downloading [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) (SSMS). There are plenty of resources available online to help you get started with this tool so I won't go into detail here. I will however provide you with the necessary details for establishing an initial connection upon opening SSMS:
- Server type = Database engine
- Server name = click on the drop down, browse for more, click the plus on database engine, select the one ending in SQL express
- Authentication = Windows authentication

### Running the Database Setup Tool
After you have installed SQL server, you need to run [this tool](https://github.com/rowanlea/RPGModernisationSeries/tree/main/Tools/DatabaseSetup) to create the tables and dummy data we will use for this project (if you've cloned the entire repo you will already have this downloaded).

### MongoDB Setup
Go here and select the platform of your choice to install [MongoDB Community](https://www.mongodb.com/try/download/community). Run the installer, select "complete" for the install type, and leave everything default.

The MongoDB installation comes with a useful tool called MongoDBCompass, which allows us to easily view and manage our data. As with SQL Server Management Studio I won't be going into detail on how to use this here, but it's pretty straightforward and simple to understand. I like Compass because it's lightweight and feels nice to use, however if you need something more powerful I suggest Studio 3T.

Note: You do not need to run any setup tools for MongoDB data, as the data will be added through the main project and the tables will be created automatically as part of the NoSQL process.

### Code
You will need to update the hardcoded logging path in FileLogger.cs to a valid location on your system. This is a temporary (yet intentional) issue that will be addressed in the near future, firstly when we implement the use of secrets, and then completely when we transition to using Azure Blob storage instead of local file storage.
