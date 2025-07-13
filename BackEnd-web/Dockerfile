# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia la solución y el csproj
COPY BackEnd-web.sln ./
COPY Backend-api/Backend-api.csproj ./Backend-api/

# Restaura dependencias
RUN dotnet restore ./Backend-api/Backend-api.csproj

# Copia el resto del código fuente
COPY Backend-api/. ./Backend-api/

# Compila y publica el proyecto en Release
WORKDIR /src/Backend-api
RUN dotnet publish -c Release -o /app --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Entity Framework CLI tools (opcional, solo si lo necesitás en producción)
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "Backend-api.dll"]
