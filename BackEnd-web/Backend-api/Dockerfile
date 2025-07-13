# Etapa 1: build con el SDK de .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el csproj y restaura dependencias de NuGet (incluido EF Core)
COPY *.csproj ./
RUN dotnet restore

# Copia el resto del código y publica en modo Release
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime con imagen liviana de ASP.NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia el resultado del build
COPY --from=build /app/publish .

# Cambiá "Backend-api.dll" por el nombre real de tu proyecto, si es distinto
ENTRYPOINT ["dotnet", "Backend-api.dll"]
