# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY BackEnd-web.sln ./
COPY Backend-api/Backend-api.csproj ./Backend-api/
RUN dotnet restore ./Backend-api/Backend-api.csproj

COPY Backend-api/. ./Backend-api/
WORKDIR /src/Backend-api
RUN dotnet publish -c Release -o /app

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Backend-api.dll"]
