FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["apps/Profio.Proxy/Profio.Proxy.csproj", "apps/Profio.Proxy/"]
COPY ["libs/Profio.Infrastructure/Profio.Infrastructure.csproj", "libs/Profio.Infrastructure/"]
COPY ["libs/Profio.Domain/Profio.Domain.csproj", "libs/Profio.Domain/"]
RUN dotnet restore "apps/Profio.Proxy/Profio.Proxy.csproj"
COPY . .
WORKDIR "/src/apps/Profio.Proxy"
RUN dotnet build "Profio.Proxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profio.Proxy.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profio.Proxy.dll"]
