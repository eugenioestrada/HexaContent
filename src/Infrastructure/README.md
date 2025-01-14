# Infrastructure Layer in Hexagon Architecture

The infrastructure layer in the Hexagon Architecture is responsible for providing technical capabilities and external dependencies required by the application. It acts as a bridge between the core domain logic and external systems such as databases, messaging systems, and other services. The infrastructure layer ensures that the core domain logic remains independent of specific technologies and frameworks.

## Goals of the Infrastructure Layer

The primary goals of the Infrastructure layer are:
- To provide technical capabilities and external dependencies required by the application.
- To act as a bridge between the core domain logic and external systems such as databases, messaging systems, and other services.
- To ensure that the core domain logic remains independent of specific technologies and frameworks.

## Database-Related Classes

The `src/Infrastructure` project contains the following database-related classes:

- `Article`: Represents an article entity with properties such as `Id`, `Title`, `Content`, `CreatedAt`, `UpdatedAt`, and a reference to the `Author`.
- `Author`: Represents an author entity with properties such as `Id`, `Name`, `Email`, `Bio`, `CreatedAt`, `UpdatedAt`, and a list of `Articles`.
- `DatabaseContext`: Represents the database context for Entity Framework Core, providing access to the `Articles` and `Authors` DbSets.

## Extension Method to Add Database Context

The `src/Infrastructure/Extension/ApplicationBuilderExtensions.cs` file contains an extension method to add the database context to the application. The `AddDbContext` method configures the `DatabaseContext` with the connection string from the configuration and uses the MySQL server version.

## Future Components in the Infrastructure Layer

The Infrastructure layer will include the following future components:
- Adapters implementing Ports for external persistence and APIs.
- Additional database-related classes and contexts.
- Messaging systems and other external services.

## Dependencies of the Infrastructure Layer

The Infrastructure layer depends on the following packages:
- `Aspire.MySqlConnector`: Provides MySQL database connectivity.
- `Aspire.RabbitMQ.Client`: Provides RabbitMQ messaging capabilities.
- `Microsoft.Extensions.DependencyInjection`: Provides dependency injection support.
- `Pomelo.EntityFrameworkCore.MySql`: Provides Entity Framework Core support for MySQL databases.

## What Must Not Be Included in the Infrastructure Layer

The Infrastructure layer must not include:
- Business logic or domain-specific logic.
- User interface or presentation logic.
- Any code that directly interacts with the user or the application's core domain logic.
