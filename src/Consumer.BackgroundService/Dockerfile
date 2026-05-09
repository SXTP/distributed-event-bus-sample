FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ["src/Consumer.BackgroundService/Consumer.BackgroundService.csproj", "src/Consumer.BackgroundService/"]
COPY ["src/Shared.Events/Shared.Events.csproj", "src/Shared.Events/"]

# Restore
RUN dotnet restore "src/Consumer.BackgroundService/Consumer.BackgroundService.csproj"

COPY . .

WORKDIR "/app/src/Consumer.BackgroundService"
RUN dotnet publish "Consumer.BackgroundService.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "Consumer.BackgroundService.dll"]