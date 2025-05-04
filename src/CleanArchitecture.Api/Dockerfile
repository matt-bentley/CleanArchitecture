FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled-extra AS base

WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080;

FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build

COPY ["src/", "/src/"]

WORKDIR /src/CleanArchitecture.Api
RUN dotnet restore "CleanArchitecture.Api.csproj" && \
    dotnet publish "CleanArchitecture.Api.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
COPY --from=build /app/publish .
USER 1000
ENTRYPOINT ["dotnet", "CleanArchitecture.Api.dll"]