# Build context is the gamesapp repository root.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build

COPY src/services/database-migrator/src/GAA.Database.Migrator/GAA.Database.Migrator.csproj src/services/database-migrator/src/GAA.Database.Migrator/
COPY src/services/database-migrator/src/GAA.Database.Migrator.Core/GAA.Database.Migrator.Core.csproj src/services/database-migrator/src/GAA.Database.Migrator.Core/

RUN dotnet restore src/services/database-migrator/src/GAA.Database.Migrator/GAA.Database.Migrator.csproj

COPY src/services/database-migrator/src/ src/services/database-migrator/src/
RUN dotnet publish src/services/database-migrator/src/GAA.Database.Migrator/GAA.Database.Migrator.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
COPY src/services/database-migrator/src/Migrations ./Migrations
ENTRYPOINT ["dotnet", "GAA.Database.Migrator.dll"]
