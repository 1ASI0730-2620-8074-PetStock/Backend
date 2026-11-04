# PetStock Platform API

RESTful API for PetStock, the inventory and sales platform for pet stores built by Nexora.

## Tech stack

- C# / .NET 10 and ASP.NET Core Web API
- Entity Framework Core with MySQL
- OpenAPI documentation with Scalar

## Bounded contexts

- IAM (Identity and Access Management)
- Profiles
- Catalog
- Inventory
- Sales
- Customers
- Purchasing
- Cash Shifts
- Analytics

## Run locally

1. Start MySQL Server (user `root`, password `12345678`).
2. `cd PetStock.API`
3. `dotnet run`
4. Open `http://localhost:<port>/scalar`