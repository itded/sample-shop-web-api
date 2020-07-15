# Sample Shop Web Api

The solution represents an implementation of the product Web Api controller.

## Technical specification
- The solution was built on .NET Core 3.1.
- The solution was configured to work with MS SQL Server (tested at 2016 version).
- The solution was written using VS Code, there are several *.vscode* folders with configuration files inside.

## The structure

The *source* folder consists of 6 projects.
- **SampleShopWebApi.Api** is the main project. It contains service configurations, the product controller and swagger settings.
- **SampleShopWebApi.Business** contains the business logic and required interface(s) to work with infrastructure. The sample contains only the product repository interface.
- **SampleShopWebApi.Data** contains an implementation of the business required interface(s), the database context, the product entity and it configuration, as well as the sql provider used to execute a seed script.
- **SampleShopWebApi.Data.Mocks** contains a mock implementation of the business required interface(s). It is used to set up the product controller in unit tests. A developer can reconfigure the product repository registration inside the **SampleShopWebApi.Api** - **Startup.cs** file.  However, a reference to the Mock project is required.
- **SampleShopWebApi.DTO** contains domain (product) and application objects (requests, results and so on).
- **SampleShopWebApi.Tests** contains the product controller tests.

The *scripts* folder contains **seed.sql**. The seed script creates the product table if not exists and inserts test data.
If the table has already exist then the script does not perform any action.

## Configuration

The **SampleShopWebApi.Api** project contains several configuration files.

### appsettings.json 
The file contains the default logging and allowed hosts configuration. Other configuration elements are:
- **ConnectionStrings** - **SampleShop** contains a default configuration string. Please to use a proper connection string to your MS SQL server database.
- **ApiControllerSettings** - **DefaultPageSize** contains a default page size in the controller responses which require paging. The page size cannot be a negative number or zero.
- **SwaggerSettings** - **Name** contains the swagger page title.

### launchsettings.json 
The file contains a launch configuration.
The default IIS Express url is *http://localhost:20977/swagger*
The default application url is *http://localhost:5000/swagger*

## Build and run
Inside the root folder please run the following commands
```cmd
dotnet build --project source\SampleShopWebApi.Api
dotnet run --project source\SampleShopWebApi.Api
```

It will build and run the configured application.\
The configuration was described in the previous section.

After the application run you can open the API definition page.\
The default url is *http://localhost:5000/swagger*

## Build and test
Inside the root folder please run the following commands
```cmd
dotnet build source\SampleShopWebApi.Tests
dotnet test source\SampleShopWebApi.Tests
```

There are 17 api controller tests.

## API specification

All API specification is available via Swagger on the API definition page.\
The default page url is *http://localhost:5000/swagger*

### Pagination
If a request requires a collection of items such as products, the response returns the addtional X-PAGINATION header.
Below there are examples of the X-PAGINATION header
```
x-pagination: {"CurrentPage":2,"PageSize":10,"TotalCount":21,"TotalPages":3,"PrevPageLink":"http://localhost:5000/api/v2.0/products?page=1&pageSize=10","NextPageLink":"http://localhost:5000/api/v2.0/products?page=3&pageSize=10"} 
```
```
x-pagination: {"CurrentPage":1,"PageSize":10,"TotalCount":21,"TotalPages":3,"PrevPageLink":"","NextPageLink":"http://localhost:5000/api/v2.0/products?page=2&pageSize=10"} 
```

