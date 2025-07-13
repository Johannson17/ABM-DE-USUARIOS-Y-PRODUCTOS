# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el .sln y el .csproj, restaura dependencias
COPY BackEnd-web.sln ./
COPY Backend-api/Backend-api.csproj ./Backend-api/
RUN dotnet restore ./Backend-api/Backend-api.csproj

# Copia todo el código
COPY Backend-api/. ./Backend-api/

# Build release
WORKDIR /src/Backend-api
RUN dotnet publish -c Release -o /app --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Si usas Entity Framework tools (opcional para CLI)
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "Backend-api.dll"]
