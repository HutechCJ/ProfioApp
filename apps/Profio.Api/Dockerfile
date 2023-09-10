FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["apps/Profio.Api/Profio.Api.csproj", "apps/Profio.Api/"]
COPY ["libs/Profio.Application/Profio.Application.csproj", "libs/Profio.Application/"]
COPY ["libs/Profio.Infrastructure/Profio.Infrastructure.csproj", "libs/Profio.Infrastructure/"]
COPY ["libs/Profio.Domain/Profio.Domain.csproj", "libs/Profio.Domain/"]
RUN dotnet restore "apps/Profio.Api/Profio.Api.csproj"
COPY . .
WORKDIR "/src/apps/Profio.Api"
RUN dotnet build "Profio.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profio.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profio.Api.dll"]
