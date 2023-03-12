FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7500

ENV ASPNETCORE_URLS=http://+:7500

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["gateway/Tasky.Gateway/Tasky.Gateway.csproj", "gateway/Tasky.Gateway/"]
RUN dotnet restore "gateway/Tasky.Gateway/Tasky.Gateway.csproj"
COPY . .
WORKDIR "/src/gateway/Tasky.Gateway"
RUN dotnet build "Tasky.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasky.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasky.Gateway.dll"]
