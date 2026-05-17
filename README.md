# GamesApp

GamesApp is a **StreamApp-style** polyrepo service layout for the Games Portal product: a consolidated **.NET 8** API, **Angular 20** single application UI, **PostgreSQL + DbUp** migrations, **Keycloak** OIDC, and optional **Loki/Grafana + Gatus** observability.

This repository is bootstrapped from patterns in `streamapp`, while the product direction comes from `volnyja-siervisy` (`game-portal` UI + `BelGamePortal` backend). **BelGamePortal is large and microservice-oriented**; this repo intentionally starts with a **single API host** plus **Shared modules** (`*.Interfaces`, `*.Implementation`, `*.Tests`) so you can port domain boundaries incrementally.

## Local URLs (Docker Compose defaults)

- **UI**: `http://localhost:4200`
- **API Swagger**: `http://localhost:5100/swagger`
- **API health**: `http://localhost:5100/health`
- **Keycloak admin**: `http://localhost:8180` (admin / admin)
- **Grafana**: `http://localhost:3200` (admin / admin from `.env.example`)
- **Gatus**: `http://localhost:8088`

## Quick start (Windows / PowerShell)

1. Copy env template:

   ```powershell
   cd .\src\gamesapp
   Copy-Item .\.env.example .\.env
   ```

2. Start infra + apps (PowerShell):

   ```powershell
   .\merge-docker-compose.ps1 up -d postgresql keycloak api ui logging monitoring
   ```

   Optional: use a non-default compose env file:

   ```powershell
   $env:GAMESAPP_ENV_FILE = ".env.prod"
   .\merge-docker-compose.ps1 up -d
   ```

3. Run migrations (one-off):

   ```powershell
   .\merge-docker-compose.ps1 run --rm database-migrator dotnet GAA.Database.Migrator.dll up
   ```

### Keycloak bootstrap (minimum)

Create a realm **`gamesapp`** and clients:

- **`gamesapp-spa`** (public): Standard flow enabled, valid redirect URIs include `http://localhost:4200/*`, web origins `http://localhost:4200`.
- **`gamesapp-api`** (confidential or bearer-only depending on your Keycloak version): used as the **JWT audience** for the API (`KEYCLOAK_AUDIENCE`).

Add a mapper on `gamesapp-spa` access token to include **audience** `gamesapp-api` if you enforce audience validation.

## Layout

- `src/services/api`: `GAA.Presentation` + `Shared/Modules/{Common,Mediator,Auth}/...`
- `src/services/ui`: Angular 20 app (`angular-oauth2-oidc` + `@ngrx/signals` session store)
- `src/services/database-migrator`: DbUp migrator + SQL under `src/Migrations/Up`

## Build (without Docker)

```powershell
dotnet build .\GAA.sln -c Release
dotnet test .\GAA.sln -c Release
cd .\src\services\ui
npm ci --legacy-peer-deps
npm run build
```

## Notes / next steps for full BelGamePortal parity

- Port vertical slices from `BelGamePortal` into the `GAA.Application` + `GAA.Infrastructure` layers (keep Shared modules cohesive).
- Expand migrations by evolving `001_InitialSchema.sql` and adding new scripts.
- If you want Promtail parity with StreamApp, add a `promtail` service similar to `streamapp` (Windows Docker bind mounts can be finicky).
