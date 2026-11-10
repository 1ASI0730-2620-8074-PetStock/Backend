# Contributing

## Branches

We use a simple GitFlow:

| Branch | Purpose |
|---|---|
| `main` | Released versions only. It is updated from `release/x.y.z` branches. |
| `develop` | Integration branch. |
| `feature/<name>`, `fix/<name>`, `chore/<name>`, `docs/<name>` | Created from an updated `develop` and merged back with a pull request to `develop`. |
| `release/x.y.z` | Created from `develop`, merged into `main`, tagged `vx.y.z` and merged back into `develop`. |

Never open a pull request from a feature branch to `main`.

## Commits

Follow [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) in English:

```
type(scope): short description
```

- **Types:** `feat`, `fix`, `chore`, `test`, `docs`, `refactor`, `style`.
- **Scopes:** `iam`, `profiles`, `catalog`, `inventory`, `sales`, `customers`, `purchasing`, `cash-shifts`, `analytics`.

Keep commits small, one step per commit.

## Code style

- Code, comments and names in English.
- One class per file. Commands, queries and resources are `record` types.
- A short `/// <summary>` on every public class and method (it is shown in Scalar).
- Follow `Catalog/` (Categories) as the template for new endpoints: query and command services, repository, controller, resources and assemblers.
- JSON field names must match the web application contract. Assemblers translate them to the model names.

## Shared files

`Program.cs`, `Resources/SharedResource.resx`, `Resources/SharedResource.es.resx` and `PetStock.API.http` have one block per bounded context. Only add lines inside your own block. Message keys start with the aggregate name (for example `SaleNotFound`) and must exist in both `.resx` files. Edit the `.resx` files as text.

If a merge conflict appears in these files, keep both sides.

## Database

Do not change the tables or add migrations without agreeing with the team. Methods on aggregates and fields on commands are fine.

## Before opening a pull request

- [ ] `dotnet build` succeeds.
- [ ] Your requests in `PetStock.API.http` return the expected codes, also with `Accept-Language: es`.
- [ ] Your endpoints appear in Scalar with their descriptions.
- [ ] No passwords, `bin/`, `obj/` or IDE files in the commits.
