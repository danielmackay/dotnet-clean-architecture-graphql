# .NET Clean Architecture with GraphQL

## Introduction

This project is a comprehensive example implementation of Clean Architecture principles using GraphQL with .NET 9. It demonstrates how to build a scalable, maintainable, and testable application with a flexible GraphQL API using modern .NET technologies and best practices.

## Architecture Overview

The solution follows Clean Architecture principles with the following layers:

- **Domain Layer** (`src/Domain`): Contains entities, value objects, domain events, and business rules
- **Application Layer** (`src/Application`): Contains use cases, interfaces, and application logic
- **Infrastructure Layer** (`src/Infrastructure`): Contains data access, external services, and infrastructure concerns
- **Presentation Layer** (`src/GraphQL`): GraphQL API endpoints and schema definitions
- **UI Layer** (`src/BlazorUI`): Blazor WebAssembly frontend application

## Technology Stack

### Backend

- **.NET 9.0** - Latest .NET framework
- **Hot Chocolate** - GraphQL server for .NET
- **Entity Framework Core** - Object-relational mapping
- **MediatR** - Mediator pattern implementation
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Validation library
- **SQL Server** - Database (with Docker support)

### Frontend

- **Blazor WebAssembly** - Frontend framework
- **MudBlazor** - Material Design component library
- **StrawberryShake** - GraphQL client for .NET

### Development Tools

- **.NET Aspire** - Cloud-ready app stack for distributed applications
- **Docker** - Containerization
- **Entity Framework Migrations** - Database schema management

## Features

### GraphQL API Features

- **Queries** with advanced capabilities:
  - Paging (cursor-based and offset-based)
  - Sorting on multiple fields
  - Filtering with complex expressions
  - Field projections to optimize data loading
- **Mutations** for data modification:
  - Create, Update, Delete operations
  - Input validation with FluentValidation
  - Unique payload types for future extensibility
- **Subscriptions** for real-time updates
- **Error handling** with custom error filters

### Domain Features

- **Todo Lists Management**: Create and manage todo lists with color coding
- **Todo Items Management**: Add, complete, and delete todo items with priorities
- **Weather Forecasts**: Sample data demonstration
- **Domain Events**: Event-driven architecture for loose coupling
- **Audit Trail**: Automatic tracking of creation and modification timestamps

## Project Structure

```text
├── src/
│   ├── Application/           # Application layer (use cases, interfaces)
│   ├── BlazorUI/             # Blazor WebAssembly frontend
│   ├── Domain/               # Domain entities, events, value objects
│   ├── GraphQL/              # GraphQL API server
│   ├── Infrastructure/       # Data access and external services
│   └── ServiceDefaults/      # .NET Aspire service defaults
├── tools/
│   ├── AppHost/              # .NET Aspire orchestration
│   └── MigrationService/     # Database migration service
└── CA.GraphQL.slnx          # Solution file
```

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for SQL Server)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) or [Rider](https://www.jetbrains.com/rider/)

### Quick Start with .NET Aspire (recommended)

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd dotnet-clean-architecture-graphql
   ```

2. **Run with .NET Aspire**

   ```bash
   cd tools/AppHost
   dotnet run
   ```

   OR

   ```bash
   aspire run 
   ```

   This will start:
   - SQL Server container
   - Database migration service
   - GraphQL API server
   - Aspire dashboard for monitoring

3. **Access the applications**
   - GraphQL API: `https://localhost:7114/graphql`
   - Aspire Dashboard: `https://localhost:15888` (URL shown in console)

### Manual Setup

1. **Start SQL Server** (using Docker)

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 2400:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
   ```

2. **Run Database Migrations**

   ```bash
   cd tools/MigrationService
   dotnet run
   ```

3. **Start the GraphQL API**

   ```bash
   cd src/GraphQL
   dotnet run
   ```

4. **Start the Blazor UI** (Optional)

   ```bash
   cd src/BlazorUI
   dotnet run
   ```

### Configuration

The application can be configured through `appsettings.json`:

- **Database**: Configure connection string for SQL Server

## GraphQL Schema

### Sample Queries

```graphql
# Get todo lists with items
query GetTodoLists {
  todoLists {
    id
    title
    colour { code }
    items {
      id
      title
      done
      priority
    }
  }
}

# Get paginated todo items with filtering
query GetTodoItems($first: Int, $where: TodoItemFilterInput) {
  todoItems(first: $first, where: $where) {
    edges {
      node {
        id
        title
        done
        priority
      }
    }
    pageInfo {
      hasNextPage
      endCursor
    }
  }
}
```

### Sample Mutations

```graphql
# Create a new todo list
mutation CreateTodoList($input: CreateTodoListInput!) {
  createTodoList(input: $input) {
    todoList {
      id
      title
    }
  }
}

# Create a todo item
mutation CreateTodoItem($input: CreateTodoItemInput!) {
  createTodoItem(input: $input) {
    todoItem {
      id
      title
      done
    }
  }
}
```

## Frontend (Blazor UI)

### Updating the GraphQL Schema

When the GraphQL schema changes, update the client-side generated code:

```bash
cd src/BlazorUI
dotnet graphql download https://localhost:7114/graphql/
```

### Features

- Responsive Material Design UI with MudBlazor
- Real-time updates via GraphQL subscriptions
- Optimistic UI updates for better user experience
- State management with StrawberryShake

## Development Guidelines

### Clean Architecture Principles

1. **Dependency Inversion**: Dependencies point inward toward the domain
2. **Separation of Concerns**: Each layer has distinct responsibilities
3. **Testability**: Business logic is isolated and easily testable

### Key Design Decisions

#### Code-First GraphQL Schema

Use Hot Chocolate 'Code-First' patterns to provide a wrapper around domain objects:

- Avoids polluting domain objects with GraphQL attributes
- Provides fine-grained control over the exposed API
- Enables schema evolution without breaking changes

#### Unique Mutation Payloads and Inputs

- All mutations return unique payload objects for future extensibility
- All mutations accept unique input objects to allow schema evolution
- Enables adding new fields without breaking existing clients

#### Domain Events

- Entities raise domain events for important business operations
- Events are processed via MediatR for loose coupling
- Enables audit trails, notifications, and other cross-cutting concerns

#### CQRS with MediatR

- Commands and Queries are separated for better scalability
- All operations go through MediatR for consistent cross-cutting concerns
- Behaviors handle validation, logging, and performance monitoring

## Testing

The solution includes comprehensive testing capabilities:

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test src/Application.Tests
```

## Deployment

### Docker Deployment

Build and run with Docker:

```bash
# Build the GraphQL API image
docker build -f src/GraphQL/Dockerfile -t graphql-api .

# Run with docker-compose (includes SQL Server)
docker-compose up
```

### Production Considerations

- Configure connection strings for production database
- Set up proper logging and monitoring
- Configure CORS policies for frontend domains
- Set up authentication and authorization as needed

## Contributing

1. Fork the repository
2. Create a feature branch
3. Follow Clean Architecture principles
4. Add appropriate tests
5. Submit a pull request

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.
