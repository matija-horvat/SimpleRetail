# Retail Procurement System

This project is a comprehensive .NET 7 Web API designed to facilitate retail procurement processes. 
It includes a RESTful API for managing store items, handling suppliers, and offering statistics. 
The aim of this assignment is to showcase expertise in API development, database design using Entity Framework Core, automated testing, Dockerization, and optional SignalR implementation or Angular integration. 
Additionally, it emphasizes the implementation of software patterns for better code organization and maintainability.

## Specific nugget packages used in project
- MediatR
- FluentValidation
- EntityFrameworkCore
- Bogus
- AutoMapper
- Newtonsoft.Json
- xUnit
- AutoFixture
- FluentAssertions
- Moq

## TODO
Besause of lack of time next systems waiting for implementation are:
- Authentification
- SignalR
- Docker Optimization

## Project Structure

### Controllers
Implement API endpoints for managing store items, suppliers, statistics, and supplier-store item relationships.

### Services
Implement business logic to handle procurement processes, statistics generation, and interaction with the database.

### DTOs (Data Transfer Objects)
Define DTOs to transfer data between the API and the client.

### Database Project
Implement the database context, models, and migrations using Entity Framework Core.

### Swagger
Demonstrate the usage of Swagger to document your APIs.

### Automated Tests
Create automated tests to ensure the reliability of the application. Utilize testing frameworks like xUnit or NUnit.

### Optional: SignalR Hub or Angular Integration
Implement SignalR Hub for real-time communication or integrate Angular for frontend development.


## Database Design

- Design a relational database schema to store information about store items, suppliers, statistics, and any other relevant entities in a retail procurement system.
- Establish appropriate relationships, including a many-to-many relationship between suppliers and store items. Each store item belongs to some supplier.
- Utilize Entity Framework Core to define the database context, models, and migrations.
- Seed the database with initial values for store items, suppliers, and relationships. Consider using libraries like Bogus for generating data.

![image info](./images/SimpleRetail_ER_diagram.png)

## Automated Testing

- Write unit tests for the API controllers to ensure proper handling of requests and responses.
- Optionally, write integration tests for the services layer to validate the correctness of business logic. Utilize a testing framework such as xUnit or NUnit.

## Software Patterns

- Implement at least three software patterns (e.g., builder, factory, generic repository, etc.) in relevant parts of your application to improve code organization and maintainability.

## Build, Run, and Test Instructions

### Prerequisites
- .NET 7 SDK installed
- Docker (optional, for Dockerization)

### Database Migration and Update
Start with setting up Connection String to you database server in <em>appSettings.json</em>.
After that, to apply database migrations and update the database schema, run the following commands:
```
dotnet ef migrations add InitialMigration
dotnet ef database update
```

### Build the Application
```
bash
dotnet build
```

### Run the Application
```
dotnet run --project SimpleRetail.API
```

### Test the Application
```
dotnet test
```




## Note
This project serves as an opportunity to demonstrate proficiency in various aspects of .NET Core development, including API design, database management, testing, and software patterns.
Feel free to reach out if you have any questions or need clarification on any aspect of the assignment.