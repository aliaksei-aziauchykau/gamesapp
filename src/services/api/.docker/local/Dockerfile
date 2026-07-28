# Build context is the gamesapp repository root (see docker-compose.yml).
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/services/api/src/GAA.Presentation/GAA.Presentation.csproj src/services/api/src/GAA.Presentation/
COPY src/services/api/src/GAA.Application/GAA.Application.csproj src/services/api/src/GAA.Application/
COPY src/services/api/src/GAA.Domain/GAA.Domain.csproj src/services/api/src/GAA.Domain/
COPY src/services/api/src/GAA.Infrastructure/GAA.Infrastructure.csproj src/services/api/src/GAA.Infrastructure/

COPY src/services/api/src/Shared/Modules/Common/GAA.Shared.Modules.Common.Interfaces/GAA.Shared.Modules.Common.Interfaces.csproj src/services/api/src/Shared/Modules/Common/GAA.Shared.Modules.Common.Interfaces/
COPY src/services/api/src/Shared/Modules/Common/GAA.Shared.Modules.Common.Implementation/GAA.Shared.Modules.Common.Implementation.csproj src/services/api/src/Shared/Modules/Common/GAA.Shared.Modules.Common.Implementation/
COPY src/services/api/src/Shared/Modules/Common/GAA.Shared.Modules.Common.Tests/GAA.Shared.Modules.Common.Tests.csproj src/services/api/src/Shared/Modules/Common/GAA.Shared.Modules.Common.Tests/
COPY src/services/api/src/Shared/Modules/Mediator/GAA.Shared.Modules.Mediator.Interfaces/GAA.Shared.Modules.Mediator.Interfaces.csproj src/services/api/src/Shared/Modules/Mediator/GAA.Shared.Modules.Mediator.Interfaces/
COPY src/services/api/src/Shared/Modules/Mediator/GAA.Shared.Modules.Mediator.Implementation/GAA.Shared.Modules.Mediator.Implementation.csproj src/services/api/src/Shared/Modules/Mediator/GAA.Shared.Modules.Mediator.Implementation/
COPY src/services/api/src/Shared/Modules/Mediator/GAA.Shared.Modules.Mediator.Tests/GAA.Shared.Modules.Mediator.Tests.csproj src/services/api/src/Shared/Modules/Mediator/GAA.Shared.Modules.Mediator.Tests/
COPY src/services/api/src/Shared/Modules/Auth/GAA.Shared.Modules.Auth.Interfaces/GAA.Shared.Modules.Auth.Interfaces.csproj src/services/api/src/Shared/Modules/Auth/GAA.Shared.Modules.Auth.Interfaces/
COPY src/services/api/src/Shared/Modules/Auth/GAA.Shared.Modules.Auth.Implementation/GAA.Shared.Modules.Auth.Implementation.csproj src/services/api/src/Shared/Modules/Auth/GAA.Shared.Modules.Auth.Implementation/
COPY src/services/api/src/Shared/Modules/Auth/GAA.Shared.Modules.Auth.Tests/GAA.Shared.Modules.Auth.Tests.csproj src/services/api/src/Shared/Modules/Auth/GAA.Shared.Modules.Auth.Tests/

COPY src/services/api/src/Shared/Microservices/GAA.Shared.Microservices.Abstractions/GAA.Shared.Microservices.Abstractions.csproj src/services/api/src/Shared/Microservices/GAA.Shared.Microservices.Abstractions/
COPY src/services/api/src/Shared/Microservices/GAA.Microservices.Composition/GAA.Microservices.Composition.csproj src/services/api/src/Shared/Microservices/GAA.Microservices.Composition/
COPY src/services/api/src/Shared/Microservices/Games/GAA.Microservices.Games/GAA.Microservices.Games.csproj src/services/api/src/Shared/Microservices/Games/GAA.Microservices.Games/
COPY src/services/api/src/Shared/Microservices/Auth/GAA.Microservices.Auth/GAA.Microservices.Auth.csproj src/services/api/src/Shared/Microservices/Auth/GAA.Microservices.Auth/
COPY src/services/api/src/Shared/Microservices/Emails/GAA.Microservices.Emails/GAA.Microservices.Emails.csproj src/services/api/src/Shared/Microservices/Emails/GAA.Microservices.Emails/
COPY src/services/api/src/Shared/Microservices/Identity/GAA.Microservices.Identity/GAA.Microservices.Identity.csproj src/services/api/src/Shared/Microservices/Identity/GAA.Microservices.Identity/
COPY src/services/api/src/Shared/Microservices/Platform/GAA.Microservices.Platform/GAA.Microservices.Platform.csproj src/services/api/src/Shared/Microservices/Platform/GAA.Microservices.Platform/

RUN dotnet restore "src/services/api/src/GAA.Presentation/GAA.Presentation.csproj"

COPY src/services/api/src/ src/services/api/src/

RUN dotnet publish "src/services/api/src/GAA.Presentation/GAA.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
ARG GIT_REVISION=unknown
ARG BUILD_TIME=unknown
LABEL org.opencontainers.image.title="gamesapp-api"
WORKDIR /app
EXPOSE 80

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .
RUN printf '%s\n' "{\"commit\":\"${GIT_REVISION}\",\"builtAt\":\"${BUILD_TIME}\",\"service\":\"gamesapp-api\"}" > /app/version.json

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "GAA.Presentation.dll"]
