# ETAPA 1 — build (SDK para compilar)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copio solo el .csproj y restauro primero (aprovecha cache de capas)
COPY ["Catalog.Api/Catalog.Api.csproj", "Catalog.Api/"]
RUN dotnet restore "Catalog.Api/Catalog.Api.csproj"

# Copio el resto y publico en Release
COPY . .
RUN dotnet publish "Catalog.Api/Catalog.Api.csproj" -c Release -o /app/publish --no-restore

# ETAPA 2 — runtime (con curl para el healthcheck)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]
