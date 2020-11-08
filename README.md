# SalesData_MicroService

This project contains a simple **.NET Core 3.1 REST API** which can handle the common HTTP requests for **Sale** objects.
It is able to handle:
- GET - For receiving all sales / For receiving a certain sale object / Additional information (sales/revenue per day/article)
- POST - For creating a new sale object
- PUT - For updating an existing sale object
- DELETE - For removing an existing sale object

The exact API documentation can be found in the included swagger file via **<URL_TO_API>/swagger**.

## Setup
For the basic implementation that uses the InMemory database, just download the solution and start it.

However the application is also able to work with a MSSQL database. To make this work make the following change in the **Startup.cs** file (Lines 24/25):
```
//services.AddDbContext<ApplicationDbContext, InMemoryDbContext>(opt => opt.UseInMemoryDatabase("SalesData_InMemory_DB"));
services.AddDbContext<ApplicationDbContext, MsSqlDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultSql")));
```
This will take the database connection string **"DefaultSql"** from the **appsettings.json** file and automatically executes the pending migrations into the database on startup of the application.<br>
**INFO**: The database for the provided connection string must exist and should be empty.

### Sample Database
There is also a sample database in the resource folder which has a few sale objects. It just has to be attached to your (localdb)\MSSQLLocalDB server.
For attaching it directly via VS2019 you could use the following approach:
- Open the Server Explorer view
- Right click on "Data Connections" -> press "Add Connection"
- Browse the database file from the Resources folder and press "OK"
- Right click on the newly added data connection and press "Close Connection"
- Switch to the SQL Server Object Explorer
- Expand databases from your (localdb)\MSSQLLocalDB server
- There the recently added database with the full path should be visible -> rename that to "SalesDb"
- Since this is the default connection string name you are good to go now ;)
