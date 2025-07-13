# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Backend-api/Backend-api.csproj ./Backend-api/
RUN dotnet restore ./Backend-api/Backend-api.csproj

COPY Backend-api/. ./Backend-api/
WORKDIR /src/Backend-api

RUN dotnet publish -c Release -o /app --no-restore

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Backend-api.dll"]
