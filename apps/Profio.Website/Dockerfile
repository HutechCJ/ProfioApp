FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["apps/Profio.Website/Profio.Website.csproj", "apps/Profio.Website/"]
RUN dotnet restore "apps/Profio.Website/Profio.Website.csproj"
COPY . .
WORKDIR "/src/apps/Profio.Website"
RUN dotnet build "Profio.Website.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profio.Website.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profio.Website.dll"]