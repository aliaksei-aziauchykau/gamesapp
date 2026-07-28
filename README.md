# GamesApp

GamesApp is a **StreamApp-style** polyrepo service layout for the Games Portal product: a consolidated **.NET 8** API, **Angular 20** single application UI, **PostgreSQL + DbUp** migrations, **Keycloak** OIDC, and optional **Loki/Grafana + Gatus** observability.

This repository is bootstrapped from patterns in `streamapp`, while the product direction comes from `volnyja-siervisy` (`game-portal` UI + `BelGamePortal` backend). **BelGamePortal is large and microservice-oriented**; this repo intentionally starts with a **single API host** plus **Shared modules** (`*.Interfaces`, `*.Implementation`, `*.Tests`) so you can port domain boundaries incrementally.

## Local URLs

**Direct ports** (no hosts / edge):

- **UI**: `http://localhost:4200` (prod image) · **UI dev**: `http://localhost:4201` (hot reload)
- **API Swagger**: `http://localhost:5100/swagger`
- **Gatus monitoring**: `http://localhost:8088`
- **Keycloak admin**: `http://localhost:8180` (admin / admin)

**StreamApp-style FQDNs** (hosts + edge Traefik on **9080**, same as `ui.streamapp.local.projects...`):

| Host | URL |
|------|-----|
| UI | `http://ui.gamesapp.local.projects.volnyja-siervisy.site:9080/` |
| UI dev | `http://ui-dev.gamesapp.local.projects.volnyja-siervisy.site:9080/` |
| API | `http://api.gamesapp.local.projects.volnyja-siervisy.site:9080/swagger` |
| Monitoring (Gatus) | `http://monitoring.gamesapp.local.projects.volnyja-siervisy.site:9080/` |

StreamApp dev UI uses `ui-dev.streamapp.local.projects...` (not `ui-dev.ui.streamapp...`).

### Hosts + deploy (recommended)

```powershell
# From monorepo root (Administrator for hosts script):
.\automation\tools\hosts\Add-GamesApp-LocalHosts.ps1
.\automation\deploy\Set-SuperServiceConfiguration.ps1 -Profile local
cd .\src\gamesapp
.\.deploy\local\Run-Local.ps1 -IncludeDevServices up -d --build
cd ..\..
.\automation\deploy\local\Deploy-Local.ps1 -Action Edge
```

Or menu option **7** / **6** in `automation\deploy\local\Deploy-Local.ps1`.

## Quick start (Windows / PowerShell)

1. Copy env template:

   ```powershell
   cd .\src\gamesapp
   Copy-Item .\.env.example .\.env
   ```

2. Start infra + apps with **hot reload** (bind mounts + `docker-compose.dev.yml`; default profile `local`):

   ```powershell
   .\merge-docker-compose.ps1 up -d postgresql keycloak
   .\merge-docker-compose.ps1 run --rm database-migrator dotnet GAA.Database.Migrator.dll up
   .\merge-docker-compose.ps1 up -d --build api ui
   ```

   UI and API source edits reload inside the containers (`ng serve` + `dotnet watch`). Requires `ROOT_WSL_PATH` in `.env` (set automatically by `merge-docker-compose.ps1` on Windows).

   To use **production-style** images instead (nginx + published API, no bind mounts), remove or rename `docker-compose.dev.yml` under `src/services/api` and `src/services/ui`, then run `up -d` as before.

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

## Game Portal UI migration

See [documentation/MIGRATION-GAME-PORTAL.md](documentation/MIGRATION-GAME-PORTAL.md) for routes, API endpoints, and follow-up work (Phaser, full BGP OData host, etc.).

## Notes / next steps for full BelGamePortal parity

- Port vertical slices from `BelGamePortal` into the `GAA.Application` + `GAA.Infrastructure` layers (keep Shared modules cohesive).
- Expand migrations by evolving `001_InitialSchema.sql` and adding new scripts.
- If you want Promtail parity with StreamApp, add a `promtail` service similar to `streamapp` (Windows Docker bind mounts can be finicky).
