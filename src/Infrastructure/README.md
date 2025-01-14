# Infrastructure Layer in Hexagon Architecture

The infrastructure layer in the Hexagon Architecture is responsible for providing technical capabilities and external dependencies required by the application. It acts as a bridge between the core domain logic and external systems such as databases, messaging systems, and other services. The infrastructure layer ensures that the core domain logic remains independent of specific technologies and frameworks.

## Database-Related Classes

The `src/Infrastructure` project contains the following database-related classes:

- `Article`: Represents an article entity with properties such as `Id`, `Title`, `Content`, `CreatedAt`, `UpdatedAt`, and a reference to the `Author`.
- `Author`: Represents an author entity with properties such as `Id`, `Name`, `Email`, `Bio`, `CreatedAt`, `UpdatedAt`, and a list of `Articles`.
- `DatabaseContext`: Represents the database context for Entity Framework Core, providing access to the `Articles` and `Authors` DbSets.

## Extension Method to Add Database Context

The `src/Infrastructure/Extension/ApplicationBuilderExtensions.cs` file contains an extension method to add the database context to the application. The `AddDbContext` method configures the `DatabaseContext` with the connection string from the configuration and uses the MySQL server version.

## Packages Referenced in HexaContent.Infrastructure.csproj

The `src/Infrastructure/HexaContent.Infrastructure.csproj` file includes references to the following packages:

- `Aspire.MySqlConnector`: Provides MySQL database connectivity.
- `Aspire.RabbitMQ.Client`: Provides RabbitMQ messaging capabilities.
- `Microsoft.Extensions.DependencyInjection`: Provides dependency injection support.
- `Pomelo.EntityFrameworkCore.MySql`: Provides Entity Framework Core support for MySQL databases.
