FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/Producer.Api/Producer.Api.csproj", "src/Producer.Api/"]
COPY ["src/Shared.Events/Shared.Events.csproj", "src/Shared.Events/"]

RUN dotnet restore "src/Producer.Api/Producer.Api.csproj"
COPY . .
WORKDIR "/src/src/Producer.Api"
RUN dotnet build "Producer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Producer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Producer.Api.dll"]