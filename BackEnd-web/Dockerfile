# Stage 1: Build - Usa el SDK completo para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de proyecto para poder restaurar paquetes sin todo el código
COPY BackEnd-web.sln ./
COPY Backend-api/Backend-api.csproj ./Backend-api/

# Restaura las dependencias (paquetes NuGet)
RUN dotnet restore ./Backend-api/Backend-api.csproj

# Copia el resto del código fuente del backend
COPY Backend-api/. ./Backend-api/

# Se mueve a la carpeta del proyecto y lo publica para producción
WORKDIR /src/Backend-api
RUN dotnet publish -c Release -o /app --no-restore

# ---

# Stage 2: Runtime - Usa la imagen liviana de ASP.NET solo para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia la aplicación ya compilada desde la etapa de "build"
COPY --from=build /app ./

# Define el comando para iniciar la aplicación
ENTRYPOINT ["dotnet", "Backend-api.dll"]