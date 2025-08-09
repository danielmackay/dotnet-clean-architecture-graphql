# .NET Clean Architecture with GraphQL

This is a comprehensive .NET 9 Clean Architecture implementation with GraphQL API using Hot Chocolate, Entity Framework Core, and Blazor WebAssembly frontend. The solution demonstrates modern .NET development practices with domain-driven design, CQRS, and event-driven architecture.

**Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

## Working Effectively

### Prerequisites and Installation
- **CRITICAL**: Install .NET 9.0 SDK first:
  ```bash
  curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version 9.0.100 --install-dir $HOME/.dotnet
  export PATH="$HOME/.dotnet:$PATH"
  ```
- Docker is required for SQL Server database (for production scenarios)
- Verify installation: `dotnet --version` should return `9.0.100`

### Building and Restoring
- **Restore .NET tools** (for GraphQL code generation):
  ```bash
  dotnet tool restore
  ```
  
- **Restore packages by dependency order** (recommended approach):
  ```bash
  cd src/Domain && dotnet restore          # Takes ~9 seconds
  cd ../Application && dotnet restore      # Takes ~7 seconds  
  cd ../Infrastructure && dotnet restore   # Takes ~30 seconds, has warnings about AutoMapper version constraints
  cd ../ServiceDefaults && dotnet restore  # Takes ~13 seconds
  cd ../GraphQL && dotnet restore          # Takes ~19 seconds, has warnings about AutoMapper version constraints
  ```

- **Build all projects**:
  ```bash
  cd src/GraphQL && dotnet build          # Takes ~16 seconds. NEVER CANCEL. Set timeout to 60+ minutes for safety.
  cd ../../tools/MigrationService && dotnet build  # Takes ~15 seconds
  cd ../AppHost && dotnet build           # Takes ~38 seconds
  ```

- **Expected warnings during build**: AutoMapper version constraint warnings and Duende.IdentityServer vulnerability warning are expected and non-blocking.

### Running the Application

#### Quick Start with .NET Aspire (Recommended)
```bash
cd tools/AppHost
dotnet run                               # NEVER CANCEL: May take 2-5 minutes to start all services
```
This automatically starts:
- SQL Server container 
- Database migration service
- GraphQL API server at https://localhost:7114
- Aspire dashboard for monitoring

**Note**: Aspire may have issues in some sandbox environments due to container orchestration requirements.

#### Manual Setup (Fallback Method)
1. **Start SQL Server**:
   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 2400:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
   ```

2. **Run Database Migrations**:
   ```bash
   cd tools/MigrationService
   dotnet run
   ```

3. **Start GraphQL API**:
   ```bash
   cd src/GraphQL
   dotnet run                            # Starts on http://localhost:5120 and https://localhost:7114
   ```

4. **Start Blazor UI** (Optional):
   ```bash
   cd src/BlazorUI
   dotnet run
   ```

#### Development Mode with In-Memory Database
For quick testing without Docker:
1. Edit `src/GraphQL/appsettings.json`: Set `"UseInMemoryDatabase": true`
2. Run: `cd src/GraphQL && dotnet run`
3. Access GraphQL Playground at: http://localhost:5120/graphql

### Testing and Validation

#### Manual Validation Scenarios
**ALWAYS perform these validation steps after making changes:**

1. **GraphQL API Health Check**:
   ```bash
   curl -X POST "http://localhost:5120/graphql" \
     -H "Content-Type: application/json" \
     -d '{"query":"{ __schema { queryType { name } } }"}'
   ```
   Expected response: `{"data":{"__schema":{"queryType":{"name":"Query"}}}}`

2. **Available Queries Test**:
   ```bash
   curl -X POST "http://localhost:5120/graphql" \
     -H "Content-Type: application/json" \
     -d '{"query":"{ __schema { queryType { fields { name } } } }"}'
   ```
   Should return: `todoItems`, `todoItem`, `todoLists` queries

3. **Test Todo Lists Query** (with in-memory DB):
   ```bash
   curl -X POST "http://localhost:5120/graphql" \
     -H "Content-Type: application/json" \
     -d '{"query":"{ todoLists { edges { node { id title } } } }"}'
   ```

4. **Access GraphQL Playground**: Navigate to http://localhost:5120/graphql in browser for interactive testing

#### No Unit Tests Available
- This repository does not contain unit test projects
- Manual validation through GraphQL queries is the primary testing method
- Always test GraphQL endpoints after code changes

### Code Generation and Schema Updates

#### Updating GraphQL Client Code (Blazor UI)
When GraphQL schema changes:
```bash
cd src/BlazorUI
dotnet graphql download https://localhost:7114/graphql/    # Download schema from running API
```

#### StrawberryShake Tool Usage
- The repository uses StrawberryShake for GraphQL client code generation
- Tool version: 14.0.0-p.15 (configured in `.config/dotnet-tools.json`)
- GraphQL files are in `src/BlazorUI/*.graphql`

## Architecture and Project Structure

### Core Projects
- **Domain** (`src/Domain`): Entities, value objects, domain events, business rules
- **Application** (`src/Application`): Use cases, CQRS handlers, interfaces  
- **Infrastructure** (`src/Infrastructure`): Data access, Entity Framework, external services
- **GraphQL** (`src/GraphQL`): Hot Chocolate GraphQL API server
- **BlazorUI** (`src/BlazorUI`): Blazor WebAssembly frontend with MudBlazor
- **ServiceDefaults** (`src/ServiceDefaults`): .NET Aspire service defaults

### Tools Projects  
- **AppHost** (`tools/AppHost`): .NET Aspire orchestration for development
- **MigrationService** (`tools/MigrationService`): Database migration runner

### Key Technologies
- **.NET 9.0** with C# 13
- **Hot Chocolate 15.1.7** for GraphQL server
- **Entity Framework Core** with SQL Server
- **MediatR** for CQRS pattern
- **AutoMapper** for object mapping
- **FluentValidation** for input validation
- **Blazor WebAssembly** with MudBlazor UI components
- **StrawberryShake** for GraphQL client

### Solution File Format
- Uses new `.slnx` format (XML-based solution file)
- Some older .NET tooling may not support this format
- Build individual projects if solution-level commands fail

## Common Issues and Workarounds

### Known Package Warnings
- **AutoMapper version constraints**: Infrastructure and GraphQL projects show NU1608 warnings about AutoMapper version conflicts - these are non-blocking
- **Duende.IdentityServer vulnerability**: NU1902 warning for moderate severity vulnerability - acknowledge but not critical for development

### Docker/Aspire Issues
- **Aspire orchestration failures**: If .NET Aspire fails in sandbox environments, use manual setup method
- **SQL Server container issues**: Ensure Docker daemon is running and has sufficient resources
- **Port conflicts**: Default ports are 5120 (HTTP), 7114 (HTTPS), 2400 (SQL Server)

### Build Performance  
- **First restore/build**: Can take 60+ seconds due to package downloads
- **Subsequent builds**: Typically 15-30 seconds
- **Clean build**: May take up to 2 minutes for all projects

## Development Guidelines

### Making Changes
- **Always build incrementally**: Test each layer (Domain → Application → Infrastructure → GraphQL)
- **Database changes**: Update Entity Framework models in Infrastructure, then run migrations
- **GraphQL schema changes**: Regenerate client code in BlazorUI project
- **UI changes**: Blazor components use MudBlazor - follow Material Design patterns

### Code Style
- **EditorConfig**: Repository has `.editorconfig` with C# formatting rules
- **Indentation**: 4 spaces for C#, 2 spaces for JSON/XML
- **Line endings**: CRLF (Windows style)

### Performance Considerations
- **Entity Framework**: Uses connection string with MultipleActiveResultSets=true
- **GraphQL**: Implements pagination with relay-style connections
- **Blazor**: WebAssembly mode for SPA experience

## Validation Checklist

Before completing any changes, verify:
- [ ] All projects build without errors: `dotnet build` in each project directory
- [ ] GraphQL API starts successfully: `cd src/GraphQL && dotnet run`
- [ ] GraphQL endpoint responds: Test with curl command above
- [ ] No new critical warnings introduced (AutoMapper warnings are expected)
- [ ] If schema changed: Regenerate GraphQL client code in BlazorUI
- [ ] Manual testing of affected GraphQL operations

**Remember**: This is a development-focused repository. Always prioritize functionality and developer experience over production deployment concerns.