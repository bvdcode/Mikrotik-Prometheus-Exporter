FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
RUN apt-get update
RUN apt-get --yes install curl
RUN apt-get clean && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Mikrotik.Exporter/Mikrotik.Exporter.csproj", "Mikrotik.Exporter/"]
RUN dotnet restore "./Mikrotik.Exporter/Mikrotik.Exporter.csproj"
COPY . .
WORKDIR "/src/Mikrotik.Exporter"
RUN dotnet build "./Mikrotik.Exporter.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mikrotik.Exporter.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
HEALTHCHECK --interval=15s --timeout=60s --retries=3 CMD curl --silent --fail http://localhost:8080/health || exit 1
ENTRYPOINT ["dotnet", "Mikrotik.Exporter.dll"]
