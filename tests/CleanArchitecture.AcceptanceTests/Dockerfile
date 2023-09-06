FROM mcr.microsoft.com/playwright:focal

RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
	dpkg -i packages-microsoft-prod.deb && \
	rm packages-microsoft-prod.deb && \
	apt-get update && \
    apt-get install -y apt-transport-https && \
    apt-get update && \
    apt-get install -y dotnet-sdk-7.0

COPY ["src/", "/src/"]
COPY ["tests/", "/tests/"]

WORKDIR /tests/CleanArchitecture.AcceptanceTests

RUN dotnet restore "CleanArchitecture.AcceptanceTests.csproj"
RUN dotnet build "CleanArchitecture.AcceptanceTests.csproj" --no-restore -c Release
ENTRYPOINT ["dotnet", "test", "-c", "Release"]