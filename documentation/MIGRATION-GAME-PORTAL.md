# Game Portal migration (volnyja-siervisy → gamesapp)

## UI (`src/services/ui`)

Migrated from `volnyja-siervisy/src/ui-apps/projects/game-portal` + inlined behavior from `@itdl/game-portal/*` libraries.

| Legacy | GamesApp |
|--------|----------|
| Nx monorepo + publishable `itdl` libs | Single Angular 20 CLI app |
| Angular 18, NgModules, NGXS, Auth0 | Standalone components, `@ngrx/signals`, Keycloak |
| Routes under `GplPagesModule` | `app.routes.ts` + `features/*` lazy routes |
| OData `https://localhost:5011/api/v1/...` | Same paths on GAA host: `/api/v1/GameDefinitions`, `/api/games/*` |

### Feature folders (routes)

- `features/home` — `/`
- `features/game-defs` — `/gamedefs`
- `features/game` — `/game?code=`
- `features/games` — `/games`
- `features/lobby` — `/lobby`
- `features/user-space` — `/space`
- `features/admin` — `/admin`
- `features/login` — `/login`

### Not yet ported (incremental)

- Phaser / Kroko / GeoGuesser game runtimes
- SSR, PWA, Stripe, SignalR, TinyMCE home CMS, full Material theming
- Full `itdl/shared` layout primitives (simplified header/footer in `layout/`)

## API (`src/services/api`)

| Layer | Status |
|-------|--------|
| `BelGamePortal/` tree | Copied; **not** wired into `GAA.Presentation` yet (Hangfire, Service Bus, EF seed differ) |
| `GAA.*` modular monolith | **Active** host |
| REST + OData aliases | `/api/games/game-definitions`, `/api/v1/GameDefinitions`, etc. |
| DB | `002_GamePortalSchema.sql` — definitions, sessions, lobbies + seed |

### Run migrations

```powershell
cd src\gamesapp
.\merge-docker-compose.ps1 run --rm database-migrator dotnet GAA.Database.Migrator.dll up
```

### Next API step

Wire `BGP.GamePortal.Api` games microservice into `GAA.Presentation` (or run BGP on `:5011` behind Traefik) when you need full OData CRUD, SignalR, and game logic modules.
