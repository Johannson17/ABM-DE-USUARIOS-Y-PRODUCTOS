# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia SOLO el proyecto
COPY Backend-api/Backend-api.csproj ./Backend-api/
RUN dotnet restore ./Backend-api/Backend-api.csproj

# Copia el resto del código fuente
COPY Backend-api/. ./Backend-api/

WORKDIR /src/Backend-api
RUN dotnet publish -c Release -o /app --no-restore

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Backend-api.dll"]
