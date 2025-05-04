FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled-extra AS base

WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build

COPY ["src/", "/src/"]

WORKDIR /src/CleanArchitecture.Migrations
RUN dotnet restore "CleanArchitecture.Migrations.csproj" && \
    dotnet publish "CleanArchitecture.Migrations.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
COPY --from=build /app/publish .
USER 1000
ENTRYPOINT ["dotnet", "CleanArchitecture.Migrations.dll"]