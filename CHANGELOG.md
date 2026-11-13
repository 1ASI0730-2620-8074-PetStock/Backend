# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Planned

- Cross-context rules (a sale reduces stock, a purchase updates the history) with domain events.
- Authentication with JWT and password hashing.

## [1.0.0] - 2026-11-13

### Added

- Sales (including delete) and payments endpoints.
- Customers and purchase history entries endpoints.
- Purchases (including delete) and goods receipts endpoints.
- Reports endpoints.
- English and Spanish messages for the new bounded contexts.
- Sample requests for sales, customers, purchasing and analytics in `PetStock.API.http`.

### Changed

- Customers query services follow the same structure as the other bounded contexts.

## [0.1.0] - 2026-11-10

### Added

- Shared kernel: `Result` pattern, base repository, unit of work, audit interceptor, `AppDbContext` with snake_case naming and kebab-case routes.
- Domain model for the 9 bounded contexts and the `InitialSchema` migration (19 tables), applied on startup.
- Categories, suppliers and products endpoints.
- Inventories (including update) and stock alerts endpoints.
- Users (sign up and update) and sessions (sign in) endpoints.
- Cash shifts (including update) and cash reconciliations endpoints.
- User preferences endpoints, one record per user.
- English and Spanish messages selected with `Accept-Language`.
- OpenAPI document and Scalar reference at `/scalar`.
- Sample requests in `PetStock.API.http`.
- `Dockerfile` and production settings with SSL for the cloud database.
