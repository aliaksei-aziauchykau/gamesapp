FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

# Restore dependencies and build the solution
RUN dotnet restore
RUN dotnet publish BGP.WebHost/BGP.WebHost.csproj -c Release -o out --property WarningLevel=0 /clp:ErrorsOnly

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .


