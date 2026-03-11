# BaseTemplate

Onion Architecture template for ASP.NET Backend API solutions.

## Project Description

(To be filled: Add your project description here)

## Technology Stack

- **.NET**: 8.0 LTS
- **Database**: Entity Framework Core with generic EF Core provider
- **API**: ASP.NET Core Minimal APIs / Controllers
- **Logging**: Serilog
- **API Documentation**: Swagger/OpenAPI
- **Testing**: xUnit

## Getting Started

1. Clone or use this template as a base
2. Run the rename script if creating from template
3. Restore dependencies: `dotnet restore`
4. Build solution: `dotnet build`
5. Update connection string in appsettings.json
6. Run API: `dotnet run --project BaseTemplate.API`

## Folder Structure

```
BaseTemplate/
├── BaseTemplate.Domain/          # Core business logic and entities
├── BaseTemplate.Application/     # Use cases and business rules
├── BaseTemplate.Infrastructure/  # Database, external services
├── BaseTemplate.API/             # Presentation layer / Controllers
└── BaseTemplate.Tests/           # Unit and integration tests
```

## Contributing

(To be filled: Add contribution guidelines)
