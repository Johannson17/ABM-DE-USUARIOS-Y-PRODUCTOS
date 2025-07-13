# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia SOLO el .csproj y restaura dependencias
COPY Backend-api.csproj ./
RUN dotnet restore

# Copia el resto del código fuente y publica
COPY . ./
RUN dotnet publish -c Release -o /app --no-restore

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

# Expone el puerto 80 (Render y la mayoría de los hostings usan este por defecto)
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "Backend-api.dll"]
