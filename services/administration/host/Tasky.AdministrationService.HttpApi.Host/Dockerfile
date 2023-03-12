FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7002

ENV ASPNETCORE_URLS=http://+:7001

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/administration/host/Tasky.AdministrationService.HttpApi.Host/Tasky.AdministrationService.HttpApi.Host.csproj", "services/administration/host/Tasky.AdministrationService.HttpApi.Host/"]
RUN dotnet restore "services/administration/host/Tasky.AdministrationService.HttpApi.Host/Tasky.AdministrationService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/administration/host/Tasky.AdministrationService.HttpApi.Host"
RUN dotnet build "Tasky.AdministrationService.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasky.AdministrationService.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasky.AdministrationService.HttpApi.Host.dll"]
