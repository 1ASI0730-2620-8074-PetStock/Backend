# PetStock Web Services

RESTful API of **PetStock**, the inventory and sales platform for small pet stores, built by **Nexora** for the Web Applications course (1ASI0730) at UPC.

| | URL |
|---|---|
| API documentation (Scalar) | https://petstock-web-services.onrender.com/scalar |
| Web application | https://petstock-frontend.onrender.com |
| Landing page | https://1asi0730-2620-8074-petstock.github.io/LandingPage/ |

## Tech stack

- C# 14 and .NET 10 (ASP.NET Core Web API)
- Entity Framework Core 10 with MySQL (`MySql.EntityFrameworkCore`)
- OpenAPI with Scalar
- Localization with `.resx` files (`en`, `es`)
- Docker, deployed on Render with a TiDB Cloud (MySQL compatible) database

## Architecture

Each bounded context is a top-level folder with four layers: `Domain`, `Application`, `Infrastructure` and `Interfaces`. Contexts reference each other only by id. `Shared` contains the `Result` pattern, the base repository, the unit of work, the audit interceptor, `AppDbContext` and the kebab-case route convention.

| Bounded context | Folder | Routes (`/api/v1`) | Status |
|---|---|---|---|
| Identity & Access Management | `IAM` | `/users`, `/sessions` | Available |
| Profile & Configuration | `Profiles` | `/preferences` | Available |
| Catalog & Supplier | `Catalog` | `/categories`, `/suppliers`, `/products` | Available |
| Inventory & Stock Monitoring | `Inventory` | `/inventories`, `/stock-alerts` | Available |
| Cash Shift Management | `CashShifts` | `/cash-shifts`, `/cash-reconciliations` | Available |
| Sales & Checkout | `Sales` | `/sales`, `/payments` | In progress |
| Customer Management | `Customers` | `/customers`, `/purchase-history-entries` | In progress |
| Purchasing & Receiving | `Purchasing` | `/purchases`, `/goods-receipts` | In progress |
| Business Analytics & Reporting | `Analytics` | `/reports` | In progress |

## Run locally

Requirements: .NET SDK 10 and MySQL 8.

```bash
dotnet run --project PetStock.API --launch-profile http
```

- API: `http://localhost:5238`
- Scalar: `http://localhost:5238/scalar`

The connection string is in `PetStock.API/appsettings.json` (database `petstock_db`). If your local MySQL password is different, override it without editing the file:

```bash
ConnectionStrings__DefaultConnection="server=localhost;user=root;password=<your-password>;database=petstock_db" \
dotnet run --project PetStock.API --launch-profile http
```

The database and its tables are created on startup from the EF Core migrations (`Database.Migrate()`). No SQL scripts are needed. Sample requests for every context are in `PetStock.API/PetStock.API.http`.

## Responses

| Code | When |
|---|---|
| 200 / 201 / 204 | Successful GET or PUT / POST / DELETE |
| 400 | Invalid data |
| 401 | Wrong credentials on sign in |
| 404 | The id does not exist |
| 409 | Duplicated data (category name, email, stock of a product, preferences of a user) |
| 500 | Unexpected error (`ProblemDetails`) |

Messages are returned in English or Spanish according to the `Accept-Language` header (English by default).

## Deployment

The `Dockerfile` builds and publishes the API on port `8080`. In production, `appsettings.Production.json` reads the database settings from environment variables:

| Variable | Description |
|---|---|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `DATABASE_HOST` / `DATABASE_PORT` | Database host and port |
| `DATABASE_USER` / `DATABASE_PASSWORD` | Database credentials |
| `DATABASE_NAME` | `petstock_db` |

The production connection requires SSL.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) and [CHANGELOG.md](CHANGELOG.md).

## License

[MIT](LICENSE)
