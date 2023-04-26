FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HelloWorld/HelloWorld.csproj", "HelloWorld/"]
RUN dotnet restore "DDNSUpdater/DDNSUpdater.csproj"
COPY . .
WORKDIR "/src/DDNSUpdater"
RUN dotnet build "DDNSUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDNSUpdater.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDNSUpdater.dll"]
