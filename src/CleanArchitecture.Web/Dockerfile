FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled-extra AS base

WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080;

FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS source

# Setup Node and NPM
RUN apt-get update && \
    apt-get install -y curl gnupg2 && \
    mkdir -p /etc/apt/keyrings && \
    curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg && \
    echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_20.x nodistro main" | tee /etc/apt/sources.list.d/nodesource.list && \
    apt-get update && \
    apt-get install -y build-essential nodejs && \
    npm install -g npm@latest

COPY ["src/", "/src/"]

FROM source AS publish
WORKDIR "/src/CleanArchitecture.Web"
RUN dotnet restore "CleanArchitecture.Web.csproj" && \
    dotnet publish "CleanArchitecture.Web.csproj" --no-restore -c Release -o /app

FROM base AS final
COPY --from=publish /app .
RUN chown -R 1000:1000 wwwroot
USER 1000
ENTRYPOINT ["dotnet", "CleanArchitecture.Web.dll"]